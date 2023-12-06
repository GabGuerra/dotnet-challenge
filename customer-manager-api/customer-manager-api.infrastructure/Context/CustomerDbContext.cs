using customer_manager_api.domain.Models;
using Microsoft.EntityFrameworkCore;

namespace customer_manager_api.infrastructure.Context
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
    }
}
