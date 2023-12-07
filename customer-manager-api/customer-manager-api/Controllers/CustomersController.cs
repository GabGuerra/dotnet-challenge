using customer_management.Requests;
using customer_manager_api.application.Services;
using customer_manager_api.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace customer_manager_api.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : BaseApiController
    {
        private readonly ICustomersService _service;

        public CustomersController(ICustomersService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] IEnumerable<CreateCustomerRequest> customersToCreate)
        {
            return await ExecutePost(async () => 
                await _service.CreateCustomersAsync(customersToCreate)
            );
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var customers = await _service.GetCustomersAsync();

            return Ok(customers);
        }
    }
}