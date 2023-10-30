using eTickets.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace eTickets.Base
{
    public class EnityBaseRepository<T> : IEnityBaseRepository<T> where T : class, IEnityBase, new()
    {
        private readonly AppDbContext _context;

        public EnityBaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T enity)
        {
             await _context.Set<T>().AddAsync(enity);
             await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var enity = await _context.Set<T>().FirstOrDefaultAsync(n => n.Id == id);
            EntityEntry entityEntry = _context.Entry<T>(enity);
            entityEntry.State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();

		public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties)
		{
			IQueryable<T> query = _context.Set<T>();
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return await query.ToListAsync();

		}

		public async Task<T> GetByIdAsync(int id)
        {
            var result = await _context.Set<T>().FirstOrDefaultAsync(n => n.Id == id);
            return result;
        }

        public async Task UpdateAsync(int id, T enity)
        {
            EntityEntry entityEntry = _context.Entry<T>(enity);
            entityEntry.State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
