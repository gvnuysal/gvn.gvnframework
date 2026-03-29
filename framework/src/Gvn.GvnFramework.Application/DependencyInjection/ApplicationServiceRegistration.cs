using System.Reflection;
using FluentValidation;
using Gvn.GvnFramework.Application.Behaviors;
using Gvn.GvnFramework.Core.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Gvn.GvnFramework.Application.DependencyInjection;

public static class ApplicationServiceRegistration
{
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
