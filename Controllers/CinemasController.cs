using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    public class CinemasController : Controller
    {
        private readonly ICinemasService _service;

        public CinemasController (ICinemasService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var allCinemas = await _service.GetAllAsync();
            return View(allCinemas);
        }
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]

		public async Task<IActionResult> Create([Bind("Logo,Name,Description")] Cinema cinema)
		{
			if (!ModelState.IsValid)
			{
				return View(cinema);
			}

			await _service.AddAsync(cinema);
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> Details(int id)
		{
			var result = await _service.GetByIdAsync(id);
			if (result == null) { return NotFound(); }
			return View(result);
		}
		public async Task<IActionResult> Edit(int id)
		{
			var resut = await _service.GetByIdAsync(id);
			if (resut == null)
			{
				return View("NotFound");
			}
			if (!ModelState.IsValid)
			{
				return View(resut);
			}
			return View(resut);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, [Bind("Logo,Name,Description")] Cinema cinema)
		{
			if (!ModelState.IsValid)
			{
				return View(cinema);
			}
			if (id == cinema.Id)
			{
				await _service.UpdateAsync(id, cinema);
				return RedirectToAction("Index");
			}
			return View(cinema);
		}

		public async Task<IActionResult> Delete(int id)
		{
			var result = await _service.GetByIdAsync(id);
			if (result == null) return View("NotFound");
			return View(result);
		}

		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeleteConfirm(int id)
		{
			var result = await _service.GetByIdAsync(id);
			if (result == null) return View("NotFound");

			await _service.DeleteAsync(id);
			return RedirectToAction(nameof(Index));

		}
	}
}
