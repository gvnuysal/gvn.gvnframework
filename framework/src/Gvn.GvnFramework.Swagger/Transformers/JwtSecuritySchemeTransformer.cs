using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Gvn.GvnFramework.Swagger.Transformers;

internal sealed class JwtSecuritySchemeTransformer(IAuthenticationSchemeProvider schemeProvider)
    : IOpenApiDocumentTransformer
{
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
