using System.Reflection;
using Gvn.GvnFramework.Modularity.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Gvn.GvnFramework.Modularity;

/// <summary>
/// Discovers, loads, and wires up <see cref="IModule"/> implementations from the supplied assemblies.
/// </summary>
public static class ModuleLoader
{
    private static readonly List<IModule> _modules = [];

    /// <summary>
    /// Scans the provided assemblies for concrete <see cref="IModule"/> implementations,
    /// instantiates each one, and calls <see cref="IModule.ConfigureServices"/> on each.
    /// </summary>
    /// <param name="services">The service collection to pass to each module.</param>
    /// <param name="assemblies">The assemblies to scan for module types.</param>
    /// <returns>The configured <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection LoadModules(
        this IServiceCollection services,
        params Assembly[] assemblies)
    {
        var moduleTypes = assemblies
            .SelectMany(a => a.GetTypes())
            .Where(t => typeof(IModule).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface);

        foreach (var moduleType in moduleTypes)
        {
            if (Activator.CreateInstance(moduleType) is IModule module)
            {
                _modules.Add(module);
                module.ConfigureServices(services);
            }
        }

        return services;
    }

    /// <summary>
    /// Calls <see cref="IModule.Configure"/> on every loaded module to set up their middleware.
    /// </summary>
    /// <param name="app">The application builder to pass to each module.</param>
    /// <returns>The configured <see cref="IApplicationBuilder"/>.</returns>
    public static IApplicationBuilder UseModules(this IApplicationBuilder app)
    {
        foreach (var module in _modules)
            module.Configure(app);

        return app;
    }

    /// <summary>
    /// Returns a read-only list of all modules that have been loaded via <see cref="LoadModules"/>.
    /// </summary>
    /// <returns>A read-only view of the loaded module instances.</returns>
    public static IReadOnlyList<IModule> GetLoadedModules() => _modules.AsReadOnly();
}
