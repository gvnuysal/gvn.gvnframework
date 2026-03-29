using MediatR;
using Microsoft.Extensions.Logging;

namespace Gvn.GvnFramework.Application.Behaviors;

/// <summary>
/// MediatR pipeline behavior that logs the incoming request and its response
/// using structured logging. Useful for tracing command and query execution.
/// </summary>
/// <typeparam name="TRequest">The request type being handled.</typeparam>
/// <typeparam name="TResponse">The response type.</typeparam>
public sealed class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    /// <summary>
    /// Logs the request before passing it to the next handler, then logs the response after it returns.
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
        var requestName = typeof(TRequest).Name;

        logger.LogInformation("Handling {RequestName}: {@Request}", requestName, request);

        var response = await next();

        logger.LogInformation("Handled {RequestName}: {@Response}", requestName, response);

        return response;
    }
}
