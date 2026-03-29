using Gvn.GvnFramework.AspNetCore.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Gvn.GvnFramework.AspNetCore.Extensions;

/// <summary>
/// Extension methods on <see cref="IApplicationBuilder"/> for registering GvnFramework middleware.
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Adds <see cref="ExceptionHandlingMiddleware"/> to the request pipeline to handle unhandled exceptions
    /// and return structured problem-details responses.
    /// </summary>
    /// <param name="app">The application builder.</param>
    /// <returns>The configured <see cref="IApplicationBuilder"/>.</returns>
    public static IApplicationBuilder UseGvnExceptionHandling(this IApplicationBuilder app)
        => app.UseMiddleware<ExceptionHandlingMiddleware>();

    /// <summary>
    /// Adds <see cref="CorrelationIdMiddleware"/> to the request pipeline to propagate or generate
    /// a correlation ID for every request.
    /// </summary>
    /// <param name="app">The application builder.</param>
    /// <returns>The configured <see cref="IApplicationBuilder"/>.</returns>
    public static IApplicationBuilder UseGvnCorrelationId(this IApplicationBuilder app)
        => app.UseMiddleware<CorrelationIdMiddleware>();
}
