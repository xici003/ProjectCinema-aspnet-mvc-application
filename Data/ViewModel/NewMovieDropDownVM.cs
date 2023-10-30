using eTickets.Models;

namespace eTickets.Data.ViewModel
{
    public class NewMovieDropDownVM
    {
        public NewMovieDropDownVM()
        {
            Producers = new List<Producer>();
            Cinemas = new List<Cinema>();
            Actors = new List<Actor>();
        }
        public List<Producer> Producers { get; set; }
        public List<Cinema> Cinemas { get; set; }
        public List<Actor> Actors { get; set; }
    }
}
