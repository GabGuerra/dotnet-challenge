using customer_manager_api.domain.Constants;
using customer_manager_api.domain.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace customer_manager_api.Controllers
{
    public class BaseApiController : ControllerBase
    {
        [NonAction]
        public async Task<IActionResult> ExecutePost(Func<Task<ApiResponse>> action)
        {
            var result = await action();

            var hasErrors = result.Errors.Any();

            if (!result.Success && hasErrors)
                return BadRequest(result);

            if (hasErrors && result.Success)
            {
                result.Status = ResponseStatuses.PartialSuccess;
                return StatusCode((int)HttpStatusCode.MultiStatus, result);
            }

            return Created(string.Empty, result);
        }
    }
}
