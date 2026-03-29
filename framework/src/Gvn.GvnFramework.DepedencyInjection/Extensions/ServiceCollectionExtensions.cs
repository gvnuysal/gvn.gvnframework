using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Gvn.GvnFramework.DepedencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
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
