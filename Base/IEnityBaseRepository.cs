using eTickets.Models;
using System.Linq.Expressions;

namespace eTickets.Base
{
    public interface IEnityBaseRepository<T> where T : class, IEnityBase, new()
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties);

        Task<T> GetByIdAsync(int id);
        Task AddAsync(T enity);
        Task UpdateAsync(int id, T enity);

        Task DeleteAsync(int id);
    }
}
