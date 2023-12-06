using customer_manager_api.domain.Models;
using customer_manager_api.domain.Repositories;
using System.Text.Json;

namespace customer_manager_api.infrastructure.Repositories
{
    public class Repository<T, TKey> : IRepository<T, TKey> where T : Entity<TKey>
    {
        public Task AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync(IEnumerable<T> entities)
        {
            //TODO: call actual repo
            foreach (var entity in entities)
            {
                Console.WriteLine($"Creating {JsonSerializer.Serialize(entity)}");
            }

            return Task.CompletedTask;
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
