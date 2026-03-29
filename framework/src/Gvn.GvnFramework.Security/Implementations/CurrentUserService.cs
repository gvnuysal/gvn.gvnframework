using System.Security.Claims;
using Gvn.GvnFramework.Security.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Gvn.GvnFramework.Security.Implementations;

public sealed class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    private ClaimsPrincipal? User => httpContextAccessor.HttpContext?.User;

    public string? UserId => User?.FindFirstValue(ClaimTypes.NameIdentifier);
    public string? Email => User?.FindFirstValue(ClaimTypes.Email);
    public IEnumerable<string> Roles => User?.FindAll(ClaimTypes.Role).Select(c => c.Value) ?? [];
    public bool IsAuthenticated => User?.Identity?.IsAuthenticated ?? false;

    public bool IsInRole(string role) => User?.IsInRole(role) ?? false;
}
