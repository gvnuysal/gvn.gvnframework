using Microsoft.AspNetCore.Http;

namespace Gvn.GvnFramework.AspNetCore.Middleware;

/// <summary>
/// ASP.NET Core middleware that reads or generates a correlation ID for each request,
/// stores it in <see cref="HttpContext.Items"/>, and echoes it back in the response headers.
/// </summary>
public sealed class CorrelationIdMiddleware(RequestDelegate next)
{
    /// <summary>The HTTP header name used to carry the correlation ID.</summary>
    public const string CorrelationIdHeader = "X-Correlation-Id";

    /// <summary>
    /// Invokes the middleware. Reads the correlation ID from the incoming request header
    /// or generates a new 16-character hex ID if absent, then propagates it to the response.
    /// </summary>
    /// <param name="context">The current HTTP context.</param>
    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue(CorrelationIdHeader, out var correlationId))
            correlationId = Guid.NewGuid().ToString("N")[..16];

        context.Items[CorrelationIdHeader] = correlationId.ToString();
        context.Response.Headers[CorrelationIdHeader] = correlationId.ToString();

        await next(context);
    }
}
