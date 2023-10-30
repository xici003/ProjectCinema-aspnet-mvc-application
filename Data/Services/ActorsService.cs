using eTickets.Base;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services
{
    public class ActorsService : EnityBaseRepository<Actor>, IActorsService
    {
        private readonly AppDbContext _context;

        public ActorsService(AppDbContext context) : base(context) { }
    }
}
