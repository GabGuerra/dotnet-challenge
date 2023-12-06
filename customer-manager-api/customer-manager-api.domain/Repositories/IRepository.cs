using customer_manager_api.domain.Models;

namespace customer_manager_api.domain.Repositories
{
    public interface IRepository<T, TKey> where T : Entity<TKey>
    {
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task<IEnumerable<T>> GetAllAsync();
    }
}
