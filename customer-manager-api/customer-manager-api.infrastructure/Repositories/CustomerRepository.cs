using customer_manager_api.domain.Models;
using customer_manager_api.domain.Repositories;

namespace customer_manager_api.infrastructure.Repositories
{
    public class CustomerRepository : Repository<Customer, int>, ICustomerRepository
    {
        public CustomerRepository()
        {

        }
    }
}
