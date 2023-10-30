using eTickets.Base;
using eTickets.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.Models
{
    public class Movie : IEnityBase
    {
        public int? Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

		[Display(Name = "Description")]
		public string Description { get; set; }
        public double Price { get; set; }

		[Display(Name = "ImageURL")]
		public string ImageURL { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Relationships
        public List<Actors_Movies> Actors_Movies { get; set; }

        //Cinema
        public int CinemaId { get; set; }
        [ForeignKey("CinemaId")]
        public Cinema Cinema { get; set; }

        //Producer
        public int ProducerId { get; set; }
        [ForeignKey("ProducerId")]
        public Producer Producer { get; set; }
        public MovieCategory MovieCategory { get; set; }
		
	}
}
