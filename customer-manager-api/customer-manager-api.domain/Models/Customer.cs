using customer_management.Requests;

namespace customer_manager_api.domain.Models
{
    public class Customer : Entity<int>
    {        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public static explicit operator Customer(CreateCustomerRequest v)
        {
            return new Customer
            {
                FirstName = v.FirstName,
                LastName = v.LastName,
                Age = v.Age,
                Id = v.Id
            };
        }
    }
}
