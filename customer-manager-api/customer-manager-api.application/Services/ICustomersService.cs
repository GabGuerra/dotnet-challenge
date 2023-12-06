using customer_management.Requests;
using customer_manager_api.domain.Models;
using customer_manager_api.domain.Responses;

namespace customer_manager_api.application.Services
{
    public interface ICustomersService
    {
        Task<ApiResponse> CreateCustomersAsync(IEnumerable<CreateCustomerRequest> customers);
        Task<IEnumerable<Customer>> GetCustomersAsync();
    }
}
