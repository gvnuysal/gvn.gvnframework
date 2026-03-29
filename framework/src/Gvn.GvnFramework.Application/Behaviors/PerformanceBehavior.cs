using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Gvn.GvnFramework.Application.Behaviors;

public sealed class PerformanceBehavior<TRequest, TResponse>(
    ILogger<PerformanceBehavior<TRequest, TResponse>> logger,
    int thresholdMs = 500)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var sw = Stopwatch.StartNew();
        var response = await next();
        sw.Stop();

        if (sw.ElapsedMilliseconds > thresholdMs)
        {
            logger.LogWarning(
                "Long running request detected: {RequestName} took {ElapsedMilliseconds}ms. Request: {@Request}",
                typeof(TRequest).Name,
                sw.ElapsedMilliseconds,
                request);
        }

        return response;
    }
}
