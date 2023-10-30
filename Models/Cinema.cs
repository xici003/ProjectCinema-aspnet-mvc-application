using eTickets.Base;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Cinema : IEnityBase
    {
        [Key]
        public int? Id { get; set; }

        [Display(Name = "Logo")]
        [Required(ErrorMessage = "Logo is required")]
        public string Logo { get; set; }

		[Display(Name = "Name")]
		[Required(ErrorMessage = "Full Name is required")]
		[StringLength(50, MinimumLength = 3, ErrorMessage = "FullName must be between 3 and 50 chars")]
		public string Name { get; set; }

		[Display(Name = "Description")]
		[Required(ErrorMessage = "Description is required")]
		public string Description { get; set; }

        // Relationships
        [ValidateNever]
        public List<Movie> Movies { get; set; }
    }
}
