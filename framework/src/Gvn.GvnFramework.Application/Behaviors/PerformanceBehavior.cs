using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Gvn.GvnFramework.Application.Behaviors;

/// <summary>
/// MediatR pipeline behavior that measures the execution time of each request and logs
/// a warning when the duration exceeds a configurable threshold.
/// </summary>
/// <typeparam name="TRequest">The request type being handled.</typeparam>
/// <typeparam name="TResponse">The response type.</typeparam>
public sealed class PerformanceBehavior<TRequest, TResponse>(
    ILogger<PerformanceBehavior<TRequest, TResponse>> logger,
    int thresholdMs = 500)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    /// <summary>
    /// Executes the next handler while measuring elapsed time and emitting a warning
    /// if execution exceeds <paramref name="thresholdMs"/> milliseconds.
    /// </summary>
    /// <param name="request">The incoming request.</param>
    /// <param name="next">The delegate representing the next step in the pipeline.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The response from the next handler.</returns>
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
