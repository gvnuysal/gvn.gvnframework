using System.Security.Claims;
using Gvn.GvnFramework.Security.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Gvn.GvnFramework.Security.Implementations;

/// <summary>
/// HTTP context-based implementation of <see cref="ICurrentUserService"/>.
/// Reads claims from the current request's <see cref="System.Security.Claims.ClaimsPrincipal"/>.
/// </summary>
public sealed class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    private ClaimsPrincipal? User => httpContextAccessor.HttpContext?.User;

    /// <inheritdoc />
    public string? UserId => User?.FindFirstValue(ClaimTypes.NameIdentifier);
    /// <inheritdoc />
    public string? Email => User?.FindFirstValue(ClaimTypes.Email);
    /// <inheritdoc />
    public IEnumerable<string> Roles => User?.FindAll(ClaimTypes.Role).Select(c => c.Value) ?? [];
    /// <inheritdoc />
    public bool IsAuthenticated => User?.Identity?.IsAuthenticated ?? false;

    /// <inheritdoc />
    public bool IsInRole(string role) => User?.IsInRole(role) ?? false;
}
