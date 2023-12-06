using customer_management.Requests;
using customer_manager_api.application.Services;
using customer_manager_api.domain.Constants;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace customer_manager_api.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersService _service;

        public CustomersController(ICustomersService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] IEnumerable<CreateCustomerRequest> customersToCreate)
        {
            var response = await _service.CreateCustomersAsync(customersToCreate);

            if (response.Status.Equals(ResponseStatuses.PartialSuccess, StringComparison.InvariantCultureIgnoreCase))
                return StatusCode((int)HttpStatusCode.MultiStatus, response);

            return StatusCode((int)HttpStatusCode.Created, response);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var customers = _service.GetCustomers();

            return Ok(customers);
        }
    }
}