using Microsoft.AspNetCore.Mvc;
using WebApi.Errors;

namespace WebApi.Controllers
{
    [Route("errors/{statusCode}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ApiControllerBase
    {
        public IActionResult Error(int statusCode)
        {
            return new ObjectResult(new ApiResponse(statusCode));
        }
    }
}
