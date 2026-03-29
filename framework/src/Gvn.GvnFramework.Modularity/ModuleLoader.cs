using System.Reflection;
using Gvn.GvnFramework.Modularity.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Gvn.GvnFramework.Modularity;

public static class ModuleLoader
{
    private static readonly List<IModule> _modules = [];

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

    public static IApplicationBuilder UseModules(this IApplicationBuilder app)
    {
        foreach (var module in _modules)
            module.Configure(app);

        return app;
    }

    public static IReadOnlyList<IModule> GetLoadedModules() => _modules.AsReadOnly();
}
