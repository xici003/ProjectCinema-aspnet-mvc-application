using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.Models
{
	public class OrderItem
	{
		[Key] 
		public int Id { get; set; }
		public int Amount { get; set; }
		public double Price { get; set; }
		public int? MovieId { get; set; }
		public virtual Movie Movie { get; set; } // Nếu viết thế này mặc định sẽ đọc từ trên xuống và sẽ hiểu MovieId là khoá ngoại

		/*public int MovieId { get; set; }
		[ForeignKey("MovieId")]
		public Movie Movie { get; set; }*/ // Cách 2 của DN khoá ngoại

		public int OrderId { get; set; }
		public virtual Order Order { get; set; }
	}
}
