using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorsService _service;

        public ActorsController(IActorsService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
		public async Task<IActionResult> Create([Bind("ProfilePictureURL,FullName,Bio")]Actor actor)
		{
			if(!ModelState.IsValid)
            {
                return View(actor);
            }
            await _service.AddAsync(actor);
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Details(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);
            if (actorDetails == null) return View("NotFound");
            return View(actorDetails);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);
            if (actorDetails == null) return View("NotFound");
            if (!ModelState.IsValid)
            {
                return View(actorDetails);
            }
            return View(actorDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit (int id,[Bind("Id,ProfilePictureURL,FullName,Bio")] Actor actor)
        {
            
            await _service.UpdateAsync(id, actor);
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);
            if (actorDetails == null) return View("NotFound");
            return View(actorDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);
            if (actorDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));

        }
    }
}
