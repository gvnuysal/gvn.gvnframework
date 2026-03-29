using Gvn.GvnFramework.Swagger.Transformers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Scalar.AspNetCore;

namespace Gvn.GvnFramework.Swagger.DependencyInjection;

/// <summary>
/// Extension methods for registering OpenAPI / Scalar UI services into the DI container and request pipeline.
/// </summary>
public static class SwaggerServiceRegistration
{
    /// <summary>
    /// Registers OpenAPI document generation and the JWT Bearer security scheme transformer.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    /// <param name="title">The API title shown in the generated document.</param>
    /// <param name="version">The API version string. Defaults to <c>"v1"</c>.</param>
    /// <param name="description">An optional description for the OpenAPI document.</param>
    /// <returns>The configured <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddGvnSwagger(
        this IServiceCollection services,
        string title,
        string version = "v1",
        string? description = null)
    {
        services.AddOpenApi(version, options =>
        {
            options.AddDocumentTransformer<JwtSecuritySchemeTransformer>();
            options.AddDocumentTransformer((document, _, _) =>
            {
                document.Info = new()
                {
                    Title = title,
                    Version = version,
                    Description = description
                };
                return Task.CompletedTask;
            });
        });

        return services;
    }

    /// <summary>
    /// Maps the OpenAPI endpoint and mounts the Scalar UI at <c>/{routePrefix}/{version}</c>.
    /// Configures C# HttpClient as the default example client and pre-selects the Bearer security scheme.
    /// </summary>
    /// <param name="app">The application builder.</param>
    /// <param name="version">The API version string. Defaults to <c>"v1"</c>.</param>
    /// <param name="routePrefix">The URL prefix for the Scalar UI. Defaults to <c>"scalar"</c>.</param>
    /// <returns>The configured <see cref="IApplicationBuilder"/>.</returns>
    public static IApplicationBuilder UseGvnSwagger(
        this IApplicationBuilder app,
        string version = "v1",
        string routePrefix = "scalar")
    {
        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapOpenApi();
            endpoints.MapScalarApiReference(options =>
            {
                options.WithTitle(string.Empty)
                       .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient)
                       .AddPreferredSecuritySchemes("Bearer");
            });
        });

        return app;
    }
}
