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
        protected readonly DbSet<T> _table;

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

            _dbContext.ChangeTracker.AutoDetectChangesEnabled = false;
            await _table.AddRangeAsync(entities);
            _dbContext.ChangeTracker.AutoDetectChangesEnabled = true;
            await _dbContext.SaveChangesAsync();

            return entities;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _table.AsNoTracking().ToListAsync();
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await _table.AsNoTracking().AnyAsync(predicate);
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await _table.AsNoTracking().Where(predicate).ToListAsync();            
        }
    }
}
