using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Gvn.GvnFramework.Modularity.Abstractions;

public interface IModule
{
    string Name { get; }
    void ConfigureServices(IServiceCollection services);
    void Configure(IApplicationBuilder app);
}
