using eTickets.Base;
using eTickets.Models;

namespace eTickets.Data.Services
{
    public class ProducersService : EnityBaseRepository<Producer>, IProducersService
    {
        private readonly AppDbContext _context;

        public ProducersService(AppDbContext context) : base(context) { }
    }
}
