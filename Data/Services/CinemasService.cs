using eTickets.Base;
using eTickets.Models;

namespace eTickets.Data.Services
{
	public class CinemasService : EnityBaseRepository<Cinema>, ICinemasService
	{
		public CinemasService(AppDbContext context) : base(context) { }
	}
}
