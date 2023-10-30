using eTickets.Data.Cart;
using eTickets.Data.Services;
using eTickets.Data.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eTickets.Controllers
{
	public class OrdersController : Controller
	{
		private readonly IMoviesService _moviesService;
		private readonly ShoppingCart _shoppingCart;
		private readonly IOrdersServices _ordersServices;

		public OrdersController(IMoviesService moviesService, ShoppingCart shoppingCart, IOrdersServices ordersServices)
		{
			_moviesService = moviesService;
			_shoppingCart = shoppingCart;
			_ordersServices = ordersServices;
		}
        public async Task<IActionResult> Index()
		{
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);

            var orders = await _ordersServices.GetOrderByUserIdAndRoleAsync(userId, userRole);
            return View(orders);
        }

        public IActionResult ShoppingCart()
		{
			var items = _shoppingCart.GetShoppingCartItems();
			_shoppingCart.ShoppingCartItems = items;

			var response = new ShoppingCartVM()
			{
				shoppingCart = _shoppingCart,
				ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
			};
			return View(response);
		}

		public async Task<IActionResult> AddToShoppingCart(int id)
		{
			var item =await _moviesService.GetMoviesByIdAsync(id);
			if(item != null)
			{
				_shoppingCart.AddItemToCart(item);
			}
			return RedirectToAction("ShoppingCart");
		}
		public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
		{
			var item = await _moviesService.GetMoviesByIdAsync(id);
			if (item != null)
			{
				_shoppingCart.RemoveItemFromCard(item);
			}
			return RedirectToAction("ShoppingCart");
		}

		public async Task<IActionResult> CompleteOrder()
		{
			var items = _shoppingCart.GetShoppingCartItems();
			string username = User.FindFirstValue(ClaimTypes.NameIdentifier);
			string emailAddress = User.FindFirstValue(ClaimTypes.Email);
			await _ordersServices.StoreOrderAsync(items, username, emailAddress);
			await _shoppingCart.ClearShoppingCartAsync();
			return View("OrderComplete");
		}

    }
}
