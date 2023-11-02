using Core.Entities;
using Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebApi.Errors;

namespace WebApi.Controllers
{
    [Route("api/buggy")]
    public class BuggyController : ApiControllerBase
    {
        [HttpGet("notfound")]
        public IActionResult NotFoundResponse([FromServices] IGenericRepository<Product> repo)
        {
            return NotFound(new ApiResponse(404));
        }

        [HttpGet("servererror")]
        public IActionResult ServerErrorResponse()
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse(500));
        }

        [HttpGet("unauthorized")]
        public IActionResult UnauthorizedResponse()
        {
            return Unauthorized(new ApiResponse(401));
        }

        [HttpGet("badRequest")]
        public IActionResult BadRequestResponse()
        {
            return BadRequest(new ValidationResponse(new[] {"error1","error2"}));
        }

        [HttpGet("exception")]
        public IActionResult ExceptionResponse()
        {
            throw new Exception("test exception");
        }
    }
}
