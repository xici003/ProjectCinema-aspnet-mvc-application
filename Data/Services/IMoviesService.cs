using eTickets.Base;
using eTickets.Data.ViewModel;
using eTickets.Models;

namespace eTickets.Data.Services
{
	public interface IMoviesService : IEnityBaseRepository<Movie>
	{
		Task<Movie> GetMoviesByIdAsync(int id);
		Task<NewMovieDropDownVM> GetNewMovieDropDownValues();

		Task AddNewMovieAsync(NewMovieVM data);

		Task UpdateNewMovieAsync(NewMovieVM data);
	}
}
