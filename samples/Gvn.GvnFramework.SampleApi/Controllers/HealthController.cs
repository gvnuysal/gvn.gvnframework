using Gvn.GvnFramework.AspNetCore.Controllers;
using Gvn.GvnFramework.Core.Results;
using Microsoft.AspNetCore.Mvc;

namespace Gvn.GvnFramework.SampleApi.Controllers;

[Route("api/[controller]")]
public sealed class HealthController : ApiControllerBase
{
    [HttpGet]
    public IActionResult Get()
        => HandleResult(Result<string>.Ok("Gvn.GvnFramework is running!"));

    [HttpGet("error-sample")]
    public IActionResult ErrorSample()
        => HandleResult(Result<string>.Fail(Error.NotFound("RESOURCE_NOT_FOUND", "Sample resource not found.")));
}
