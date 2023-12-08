using customer_management.Requests;
using customer_manager_api.domain.Models;
using customer_manager_api.domain.Responses;
using Refit;

namespace customer_manager_api_consumer.Api
{
    public interface ICustomerManagerApiClient
    {
        [Post("")]
        Task<ApiResponse> CreateCustomersAsync(IEnumerable<CreateCustomerRequest> customers);
        [Get("")]
        Task<DataResponse<Customer[]>> GetCustomersAsync();
    }
}
