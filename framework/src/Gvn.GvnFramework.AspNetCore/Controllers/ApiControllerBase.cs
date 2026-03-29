using Gvn.GvnFramework.Core.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gvn.GvnFramework.AspNetCore.Controllers;

/// <summary>
/// Base class for API controllers that provides helper methods for translating
/// <see cref="Result"/> and <see cref="Result{T}"/> objects into HTTP responses.
/// </summary>
[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
    /// <summary>
    /// Converts a non-generic <see cref="Result"/> to an <see cref="IActionResult"/>.
    /// Returns <c>200 OK</c> on success, or an appropriate error status code on failure.
    /// </summary>
    /// <param name="result">The result to translate.</param>
    /// <returns>An <see cref="IActionResult"/> that reflects the outcome of the operation.</returns>
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

    /// <summary>
    /// Converts a generic <see cref="Result{T}"/> to an <see cref="IActionResult"/>.
    /// Returns <c>200 OK</c> with the data payload on success, or an appropriate error status code on failure.
    /// </summary>
    /// <typeparam name="T">The type of the result data payload.</typeparam>
    /// <param name="result">The result to translate.</param>
    /// <returns>An <see cref="IActionResult"/> that reflects the outcome of the operation.</returns>
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
