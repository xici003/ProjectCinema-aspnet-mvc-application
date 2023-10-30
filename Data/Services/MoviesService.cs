using eTickets.Base;
using eTickets.Data.ViewModel;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services
{
	public class MoviesService : EnityBaseRepository<Movie>, IMoviesService
	{
		private readonly AppDbContext _context;
		public MoviesService(AppDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task AddNewMovieAsync(NewMovieVM data)
		{
			var newMovie = new Movie()
			{
				Name = data.Name,
				Description = data.Description,
				Price = data.Price,
				ImageURL = data.ImageURL,
				CinemaId = data.CinemaId,
				StartDate = data.StartDate,
				EndDate = data.EndDate,
				MovieCategory = data.MovieCategory,
				ProducerId = data.ProducerId
			};
			await _context.Movies.AddAsync(newMovie);
			await _context.SaveChangesAsync();

			//Add Movie Actors
			foreach (var actorId in data.ActorIds)
			{
				var newActorMovie = new Actors_Movies()
				{
					MovieId = newMovie.Id,
					ActorId = actorId
				};
				await _context.Actors_Movies.AddAsync(newActorMovie);
			}
			await _context.SaveChangesAsync();
		}

		public async Task<Movie> GetMoviesByIdAsync(int id)
		{
			var moviesDetails = await _context.Movies.Include(n => n.Cinema)
												.Include(m => m.Producer)
												.Include(am => am.Actors_Movies).ThenInclude(a => a.Actor)
												.FirstOrDefaultAsync(i => i.Id == id);
			return moviesDetails;
		}

		public async Task<NewMovieDropDownVM> GetNewMovieDropDownValues()
		{
			var response = new NewMovieDropDownVM()
			{
				Actors = await _context.Actors.OrderBy(a => a.FullName).ToListAsync(),
				Cinemas = await _context.Cinemas.OrderBy(c => c.Name).ToListAsync(),
				Producers = await _context.Producers.OrderBy(p => p.FullName).ToListAsync()
		};
			return response;
        }

		public async Task UpdateNewMovieAsync(NewMovieVM data)
		{
			var dbMovie = await _context.Movies.FirstOrDefaultAsync(n => n.Id == data.Id);

			if (dbMovie != null)
			{
				dbMovie.Name = data.Name;
				dbMovie.Description = data.Description;
				dbMovie.Price = data.Price;
				dbMovie.ImageURL = data.ImageURL;
				dbMovie.CinemaId = data.CinemaId;
				dbMovie.StartDate = data.StartDate;
				dbMovie.EndDate = data.EndDate;
				dbMovie.MovieCategory = data.MovieCategory;
				dbMovie.ProducerId = data.ProducerId;
				await _context.SaveChangesAsync();
			}

			//Remove existing actors
			var existingActorsDb = _context.Actors_Movies.Where(n => n.MovieId == data.Id).ToList();
			_context.Actors_Movies.RemoveRange(existingActorsDb);
			await _context.SaveChangesAsync();

			//Add Movie Actors
			foreach (var actorId in data.ActorIds)
			{
				var newActorMovie = new Actors_Movies()
				{
					MovieId = data.Id,
					ActorId = actorId
				};
				await _context.Actors_Movies.AddAsync(newActorMovie);
			}
			await _context.SaveChangesAsync();
		}
	
	}
}
