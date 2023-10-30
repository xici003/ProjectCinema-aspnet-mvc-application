using Microsoft.EntityFrameworkCore;

namespace eTickets.Models
{
    public class Actors_Movies 
    {
        public int ActorId { get; set; }
        public Movie Movie { get; set; }
        public int? MovieId { get; set; }
        public Actor Actor { get; set; }

       

    }
}
