using System.Reflection;
using FluentValidation;
using Gvn.GvnFramework.Application.Behaviors;
using Gvn.GvnFramework.Core.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Gvn.GvnFramework.Application.DependencyInjection;

/// <summary>
/// Provides extension methods to register all Application layer services into the DI container.
/// </summary>
public static class ApplicationServiceRegistration
{
    /// <summary>
    /// Registers MediatR, FluentValidation validators, and all pipeline behaviors
    /// (logging, validation, performance) from the given assemblies.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    /// <param name="assemblies">
    /// The assemblies to scan for MediatR handlers and FluentValidation validators.
    /// </param>
    /// <returns>The same <see cref="IServiceCollection"/> for chaining.</returns>
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        params Assembly[] assemblies)
    {
        services.AddMediatR(cfg =>
        {
            foreach (var assembly in assemblies)
                cfg.RegisterServicesFromAssembly(assembly);
        });

        services.AddValidatorsFromAssemblies(assemblies, ServiceLifetime.Transient);

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));

        return services;
    }
}
