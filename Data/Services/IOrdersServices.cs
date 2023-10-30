using eTickets.Models;

namespace eTickets.Data.Services
{
    public interface IOrdersServices
    {
        Task StoreOrderAsync(List<ShoppingCartItem> items,string userId, string userEmailAddress);
        Task< List<Order> > GetOrderByUserIdAndRoleAsync(string userId,string userRole);
    }
}
