namespace Gvn.GvnFramework.Security.Abstractions;

public interface ICurrentUserService
{
    string? UserId { get; }
    string? Email { get; }
    IEnumerable<string> Roles { get; }
    bool IsAuthenticated { get; }
    bool IsInRole(string role);
}
