using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Gvn.GvnFramework.DepedencyInjection.Extensions;

/// <summary>
/// Extension methods on <see cref="IServiceCollection"/> for advanced service registration patterns.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Scans <paramref name="assembly"/> for all concrete types that implement <typeparamref name="TInterface"/>
    /// and registers each one against its own direct interfaces (excluding <typeparamref name="TInterface"/> itself).
    /// </summary>
    /// <typeparam name="TInterface">The marker interface used to locate implementations.</typeparam>
    /// <param name="services">The service collection to register into.</param>
    /// <param name="assembly">The assembly to scan.</param>
    /// <param name="lifetime">The service lifetime for all discovered registrations. Defaults to <see cref="ServiceLifetime.Scoped"/>.</param>
    /// <returns>The configured <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection RegisterAllImplementations<TInterface>(
        this IServiceCollection services,
        Assembly assembly,
        ServiceLifetime lifetime = ServiceLifetime.Scoped)
    {
        var implementations = assembly.GetTypes()
            .Where(t => typeof(TInterface).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface);

        foreach (var implementation in implementations)
        {
            var interfaces = implementation.GetInterfaces().Where(i => i != typeof(TInterface));
            foreach (var iface in interfaces)
                services.Add(new ServiceDescriptor(iface, implementation, lifetime));
        }

        return services;
    }

    /// <summary>
    /// Wraps the existing <typeparamref name="TInterface"/> registration with a <typeparamref name="TDecorator"/>.
    /// The decorator receives the original service as a constructor parameter.
    /// </summary>
    /// <typeparam name="TInterface">The service type to decorate. Must already be registered.</typeparam>
    /// <typeparam name="TDecorator">The decorator type. Must implement <typeparamref name="TInterface"/>.</typeparam>
    /// <param name="services">The service collection to update.</param>
    /// <returns>The configured <see cref="IServiceCollection"/>.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when <typeparamref name="TInterface"/> is not registered in <paramref name="services"/>.
    /// </exception>
    public static IServiceCollection Decorate<TInterface, TDecorator>(
        this IServiceCollection services)
        where TDecorator : class, TInterface
        where TInterface : class
    {
        var descriptor = services.FirstOrDefault(d => d.ServiceType == typeof(TInterface))
            ?? throw new InvalidOperationException($"Service of type {typeof(TInterface).Name} is not registered.");

        var objectFactory = ActivatorUtilities.CreateFactory(
            typeof(TDecorator),
            [typeof(TInterface)]);

        services.Replace(new ServiceDescriptor(
            typeof(TInterface),
            sp => (TInterface)objectFactory(sp, [sp.CreateInstance(descriptor)]),
            descriptor.Lifetime));

        return services;
    }

    private static object CreateInstance(this IServiceProvider services, ServiceDescriptor descriptor)
    {
        if (descriptor.ImplementationInstance is not null)
            return descriptor.ImplementationInstance;

        if (descriptor.ImplementationFactory is not null)
            return descriptor.ImplementationFactory(services);

        return ActivatorUtilities.GetServiceOrCreateInstance(
            services, descriptor.ImplementationType!);
    }
}
