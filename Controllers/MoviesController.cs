using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection.Metadata;

namespace eTickets.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesService _service;

        public MoviesController(IMoviesService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var allMovies = await _service.GetAllAsync(n => n.Cinema);
            return View(allMovies);
        }

        //GET :Movies/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var moviesDetail = await _service.GetMoviesByIdAsync(id);
            return View(moviesDetail);
        }

        //GET: Movies/Create
        public async Task<IActionResult> Create( )
        {
            var movieDropdownsData = await _service.GetNewMovieDropDownValues();

            ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownsData.Producers,"Id","FullName");
            ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Create(NewMovieVM data)
        {
            if (!ModelState.IsValid)
            {
				var movieDropdownsData = await _service.GetNewMovieDropDownValues();

				ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
				ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
				ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");
				return View(data);
            }
            await _service.AddNewMovieAsync(data);
            return RedirectToAction("Index");

        }

		//GET: Movies/Edit/1
		public async Task<IActionResult> Edit(int id)
		{
			var movieDetails = await _service.GetMoviesByIdAsync(id);
			if (movieDetails == null) return View("NotFound");

			var response = new NewMovieVM()
			{
				Id = movieDetails.Id,
				Name = movieDetails.Name,
				Description = movieDetails.Description,
				Price = movieDetails.Price,
				StartDate = movieDetails.StartDate,
				EndDate = movieDetails.EndDate,
				ImageURL = movieDetails.ImageURL,
				MovieCategory = movieDetails.MovieCategory,
				CinemaId = movieDetails.CinemaId,
				ProducerId = movieDetails.ProducerId,
				ActorIds = movieDetails.Actors_Movies.Select(n => n.ActorId).ToList(),
			};

			var movieDropdownsData = await _service.GetNewMovieDropDownValues();
			ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
			ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
			ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

			return View(response);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, NewMovieVM movie)
		{
			if (id != movie.Id) return View("NotFound");

			if (!ModelState.IsValid)
			{
				var movieDropdownsData = await _service.GetNewMovieDropDownValues();

				ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
				ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
				ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

				return View(movie);
			}

			await _service.UpdateNewMovieAsync(movie);
			return RedirectToAction(nameof(Index));
		}

		// Search
		public async Task<IActionResult> Filter(String searchString)
		{
			var allMovies = await _service.GetAllAsync(n => n.Cinema);
			if(!string.IsNullOrEmpty(searchString))
			{
				var filteredResult = allMovies.Where(n => n.Name.Contains(searchString) || n.Description.Contains(searchString)).ToList();
				return View("Index",filteredResult);
			}
			return View("Index",allMovies);
		}
	}
}
