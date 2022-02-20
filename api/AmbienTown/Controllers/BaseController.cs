using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TimeSheet.Api.Utils.Result;

namespace TimeSheet.Api.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected IActionResult ErrorResponse(Result result)
        {
            return result.Type switch
            {
                OperationResultType.ValidationError => BadRequest(result),
                OperationResultType.NotFound => NotFound(result),
                OperationResultType.Conflict => Conflict(result),
                OperationResultType.Forbidden => Forbid(),
                OperationResultType.Unauthorized => Unauthorized(result),
                _ => StatusCode(500, result),
            };
        }
    }
}