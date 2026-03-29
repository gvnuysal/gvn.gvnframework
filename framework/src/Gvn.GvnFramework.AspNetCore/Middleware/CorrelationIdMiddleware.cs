using Microsoft.AspNetCore.Http;

namespace Gvn.GvnFramework.AspNetCore.Middleware;

public sealed class CorrelationIdMiddleware(RequestDelegate next)
{
    public const string CorrelationIdHeader = "X-Correlation-Id";

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue(CorrelationIdHeader, out var correlationId))
            correlationId = Guid.NewGuid().ToString("N")[..16];

        context.Items[CorrelationIdHeader] = correlationId.ToString();
        context.Response.Headers[CorrelationIdHeader] = correlationId.ToString();

        await next(context);
    }
}
