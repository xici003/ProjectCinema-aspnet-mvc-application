using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services
{
    public class OrdersService : IOrdersServices
    {
        private readonly AppDbContext _context;

        public OrdersService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetOrderByUserIdAndRoleAsync(string userId,string userRole)
        {
            var items = await _context.Orders.Include(n => n.OrderItems).ThenInclude(n => n.Movie).Include(n => n.User).ToListAsync();  
            if(userRole != "Admin")
            {
                items = items.Where(n => n.UserId == userId).ToList();
            }
            return items;
        }
 
        public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress)
        {
            var order = new Order()
            {
                UserId = userId,
                Email = userEmailAddress
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            foreach(var item in items)
            {
                var orderItem = new OrderItem()
                {
                    Amount = item.Amount,
                    MovieId = item.Movie.Id,
                    OrderId = order.Id,
                    Price = item.Movie.Price
                };
                await _context.OrderItems.AddAsync(orderItem);
            }
            await _context.SaveChangesAsync();
        }
    }
}
