using customer_manager_api.domain.Models;
using customer_manager_api.domain.Repositories;
using customer_manager_api.infrastructure.Context;

namespace customer_manager_api.infrastructure.Repositories
{
    public class CustomerRepository : Repository<Customer, int>, ICustomerRepository
    {
        public CustomerRepository(CustomerDbContext context) : base(context)
        {

        }
    }
}
