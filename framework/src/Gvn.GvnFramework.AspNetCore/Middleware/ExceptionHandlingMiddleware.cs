using System.Text.Json;
using Gvn.GvnFramework.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Gvn.GvnFramework.AspNetCore.Middleware;

/// <summary>
/// ASP.NET Core middleware that catches unhandled exceptions and converts them to
/// RFC 7807 problem-details JSON responses with appropriate HTTP status codes.
/// </summary>
public sealed class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    /// <summary>
    /// Invokes the middleware, delegating to the next component in the pipeline.
    /// Catches any unhandled exception and writes a structured error response.
    /// </summary>
    /// <param name="context">The current HTTP context.</param>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (statusCode, title, detail) = exception switch
        {
            NotFoundException e => (StatusCodes.Status404NotFound, "Resource Not Found", e.Message),
            ValidationException e => (StatusCodes.Status400BadRequest, "Validation Error", e.Message),
            UnauthorizedException e => (StatusCodes.Status401Unauthorized, "Unauthorized", e.Message),
            ConflictException e => (StatusCodes.Status409Conflict, "Conflict", e.Message),
            GvnException e => (StatusCodes.Status400BadRequest, "Bad Request", e.Message),
            _ => (StatusCodes.Status500InternalServerError, "Internal Server Error", "An unexpected error occurred.")
        };

        var problemDetails = new
        {
            type = $"https://httpstatuses.io/{statusCode}",
            title,
            status = statusCode,
            detail,
            instance = context.Request.Path.Value
        };

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/problem+json";

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(problemDetails,
                new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
    }
}
