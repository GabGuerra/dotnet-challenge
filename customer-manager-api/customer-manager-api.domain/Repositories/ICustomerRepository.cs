using customer_manager_api.domain.Models;

namespace customer_manager_api.domain.Repositories
{
    public interface ICustomerRepository : IRepository<Customer, int>
    {
    }
}
