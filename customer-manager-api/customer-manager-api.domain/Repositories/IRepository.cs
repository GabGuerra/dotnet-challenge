using customer_manager_api.domain.Models;
using System.Linq.Expressions;

namespace customer_manager_api.domain.Repositories
{
    public interface IRepository<T, TKey> where T : Entity<TKey>
    {
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
    }
}
