using eTickets.Base;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Producer : IEnityBase
    {
        [Key]
        public int? Id { get; set; }

        [Display(Name = "ProfilePictureURL")]
		[Required(ErrorMessage = "Profile Picture is required")]
		public string ProfilePictureURL { get; set; }

		[Display(Name = "FullName")]
		[Required(ErrorMessage = "Full Name is required")]
		[StringLength(50, MinimumLength = 3, ErrorMessage = "FullName must be between 3 and 50 chars")]
		public string FullName { get; set; }

		[Display(Name = "Bio")]
		[Required(ErrorMessage = "Biography is required")]
		public string Bio { get; set; }

        // Relationships

        public List<Movie>? Movies { get; set; }
    }
}
