using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Gvn.GvnFramework.Modularity.Abstractions;

/// <summary>
/// Defines a self-contained application module that registers its own services
/// and configures the request pipeline.
/// Implement this interface to create a pluggable feature module and register it
/// with <see cref="ModuleLoader.LoadModules"/>.
/// </summary>
public interface IModule
{
    /// <summary>Gets the display name of the module.</summary>
    string Name { get; }

    /// <summary>
    /// Registers the module's services into the DI container.
    /// Called during application startup before the host is built.
    /// </summary>
    /// <param name="services">The service collection to add services to.</param>
    void ConfigureServices(IServiceCollection services);

    /// <summary>
    /// Configures the module's middleware and endpoints in the request pipeline.
    /// Called after the host is built.
    /// </summary>
    /// <param name="app">The application builder to configure.</param>
    void Configure(IApplicationBuilder app);
}
