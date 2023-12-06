using customer_manager_api.domain.Models;
using customer_manager_api.domain.Repositories;
using customer_manager_api.infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace customer_manager_api.infrastructure.Repositories
{
    public class Repository<T, TKey> : IRepository<T, TKey> where T : Entity<TKey>
    {
        protected CustomerDbContext _dbContext;
        private readonly DbSet<T> _table;

        public Repository(CustomerDbContext customerDbContext)
        {
            _dbContext = customerDbContext;
            _table = _dbContext.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            _table.Add(entity);

            await _dbContext.SaveChangesAsync();

            return entity;
        }
        
        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _table.AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();

            return entities;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _table.ToListAsync();
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await _table.AnyAsync(predicate);
        }
    }
}
