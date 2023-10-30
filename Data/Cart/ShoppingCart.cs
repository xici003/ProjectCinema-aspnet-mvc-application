using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Cart
{
	public class ShoppingCart
	{
		//Add data to SC and remove it from SC
		public AppDbContext _context { get; set; }
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

		public ShoppingCart(AppDbContext context)
		{
			_context = context;
		}
        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<AppDbContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }
        public void AddItemToCart (Movie movie)
		{
			var shoppingCartItem = _context.shoppingCartItems.FirstOrDefault(n => n.Movie.Id  == movie.Id && n.ShoppingCartId == ShoppingCartId);	
			if (shoppingCartItem == null)
			{
				shoppingCartItem = new ShoppingCartItem()
				{
					ShoppingCartId = ShoppingCartId,
					Movie = movie,
					Amount = 1
				};

				_context.shoppingCartItems.Add(shoppingCartItem);
			}else
			{
				shoppingCartItem.Amount++;
			}
			_context.SaveChanges();
		}

		public void RemoveItemFromCard (Movie movie)
		{
			var shoppingCartItem = _context.shoppingCartItems.FirstOrDefault(n => n.Movie.Id == movie.Id && n.ShoppingCartId == ShoppingCartId);
			if (shoppingCartItem != null)
			{
				if(shoppingCartItem.Amount > 1)
				{
					shoppingCartItem.Amount --;
				}
				else
				{
					_context.shoppingCartItems.Remove(shoppingCartItem);
				}
			}

			_context.SaveChanges();
		}
		public List<ShoppingCartItem> GetShoppingCartItems()
		{
			return ShoppingCartItems ?? (ShoppingCartItems = _context.shoppingCartItems.Where(n => 
			n.ShoppingCartId == ShoppingCartId).Include(n => n.Movie).ToList());
		}

		public int GetAmount() => _context.shoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).Select(n => n.Amount).Sum();

		public double GetShoppingCartTotal() => _context.shoppingCartItems.Where(n => 
		n.ShoppingCartId == ShoppingCartId).Select(n => n.Movie.Price * n.Amount).Sum();

		public async Task ClearShoppingCartAsync()
		{
			var items = await _context.shoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).ToListAsync();
			_context.shoppingCartItems.RemoveRange(items);
			await _context.SaveChangesAsync();
		}
    }
}
