using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Gvn.GvnFramework.Swagger.Transformers;

/// <summary>
/// OpenAPI document transformer that injects a JWT Bearer security scheme definition
/// when the application has a "Bearer" authentication scheme registered.
/// </summary>
internal sealed class JwtSecuritySchemeTransformer(IAuthenticationSchemeProvider schemeProvider)
    : IOpenApiDocumentTransformer
{
    /// <summary>
    /// Adds a <c>Bearer</c> HTTP security scheme to <see cref="OpenApiComponents.SecuritySchemes"/>
    /// if Bearer authentication is present in the application's scheme registry.
    /// </summary>
    /// <param name="document">The OpenAPI document being built.</param>
    /// <param name="context">Contextual information about the transformation.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public async Task TransformAsync(
        OpenApiDocument document,
        OpenApiDocumentTransformerContext context,
        CancellationToken cancellationToken)
    {
        var authSchemes = await schemeProvider.GetAllSchemesAsync();
        var hasJwt = authSchemes.Any(s => s.Name == "Bearer");

        if (!hasJwt) return;

        var securityScheme = new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Bearer token. Example: eyJhbGci..."
        };

        document.Components ??= new OpenApiComponents();
        document.Components.SecuritySchemes["Bearer"] = securityScheme;
    }
}
