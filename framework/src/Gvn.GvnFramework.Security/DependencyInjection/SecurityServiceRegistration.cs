using System.Text;
using Gvn.GvnFramework.Security.Abstractions;
using Gvn.GvnFramework.Security.Configuration;
using Gvn.GvnFramework.Security.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Gvn.GvnFramework.Security.DependencyInjection;

/// <summary>
/// Extension methods for registering security services into the DI container.
/// </summary>
public static class SecurityServiceRegistration
{
    /// <summary>
    /// Registers JWT authentication, authorization, <see cref="ITokenService"/>,
    /// <see cref="IPasswordHasher"/>, and <see cref="ICurrentUserService"/> using the provided configuration action.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    /// <param name="configure">A delegate to configure <see cref="JwtOptions"/>.</param>
    /// <returns>The configured <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddGvnSecurity(
        this IServiceCollection services,
        Action<JwtOptions> configure)
    {
        var jwtOptions = new JwtOptions();
        configure(jwtOptions);
        services.Configure<JwtOptions>(o =>
        {
            o.Secret = jwtOptions.Secret;
            o.Issuer = jwtOptions.Issuer;
            o.Audience = jwtOptions.Audience;
            o.ExpiryMinutes = jwtOptions.ExpiryMinutes;
        });

        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddSingleton<ITokenService, JwtTokenService>();
        services.AddSingleton<IPasswordHasher, BcryptPasswordHasher>();

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(bearerOptions =>
            {
                bearerOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret)),
                    ValidateIssuer = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtOptions.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

        services.AddAuthorization();

        return services;
    }
}
