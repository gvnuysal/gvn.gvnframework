using Gvn.GvnFramework.Swagger.Transformers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Scalar.AspNetCore;

namespace Gvn.GvnFramework.Swagger.DependencyInjection;

public static class SwaggerServiceRegistration
{
    /// <summary>
    /// OpenAPI belge üretimini ve Scalar UI'ı ekler.
    /// </summary>
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
    /// OpenAPI endpoint'ini ve Scalar UI'ı pipeline'a ekler.
    /// Varsayılan adres: /scalar/{version}
    /// </summary>
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
