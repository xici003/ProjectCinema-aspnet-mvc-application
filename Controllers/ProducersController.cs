using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    public class ProducersController : Controller
    {
        private readonly IProducersService _service;

        public ProducersController(IProducersService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var allProducers = await _service.GetAllAsync();
            return View(allProducers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create([Bind("ProfilePictureURL,FullName,Bio")] Producer producer)
        {
            if (!ModelState.IsValid)
            {
                return View(producer);
            }

            await _service.AddAsync(producer);
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
        public async Task<IActionResult> Edit(int id, [Bind("ProfilePictureURL,FullName,Bio")] Producer producer)
        {
            if (!ModelState.IsValid)
            {
                return View(producer);
            }
            if (id == producer.Id)
            {
                await _service.UpdateAsync(id, producer);
                return RedirectToAction("Index");
            }
            return View(producer);
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

