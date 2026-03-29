using Gvn.GvnFramework.Core.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gvn.GvnFramework.AspNetCore.Controllers;

[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
    protected IActionResult HandleResult(Result result)
    {
        if (result.Succeeded)
            return Ok();

        return result.FirstError?.Type switch
        {
            ErrorType.NotFound => NotFound(result.Errors),
            ErrorType.Validation => BadRequest(result.Errors),
            ErrorType.Unauthorized => Unauthorized(result.Errors),
            ErrorType.Conflict => Conflict(result.Errors),
            _ => StatusCode(StatusCodes.Status500InternalServerError, result.Errors)
        };
    }

    protected IActionResult HandleResult<T>(Result<T> result)
    {
        if (result.Succeeded)
            return Ok(result.Data);

        return result.FirstError?.Type switch
        {
            ErrorType.NotFound => NotFound(result.Errors),
            ErrorType.Validation => BadRequest(result.Errors),
            ErrorType.Unauthorized => Unauthorized(result.Errors),
            ErrorType.Conflict => Conflict(result.Errors),
            _ => StatusCode(StatusCodes.Status500InternalServerError, result.Errors)
        };
    }
}
