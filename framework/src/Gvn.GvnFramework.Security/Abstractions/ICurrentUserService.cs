namespace Gvn.GvnFramework.Security.Abstractions;

/// <summary>
/// Provides information about the currently authenticated user extracted from the HTTP context.
/// </summary>
public interface ICurrentUserService
{
    /// <summary>Gets the unique identifier of the authenticated user, or <c>null</c> if unauthenticated.</summary>
    string? UserId { get; }

    /// <summary>Gets the email address of the authenticated user, or <c>null</c> if unauthenticated.</summary>
    string? Email { get; }

    /// <summary>Gets the roles assigned to the authenticated user.</summary>
    IEnumerable<string> Roles { get; }

    /// <summary>Gets a value indicating whether the current request is authenticated.</summary>
    bool IsAuthenticated { get; }

    /// <summary>
    /// Determines whether the current user belongs to the specified role.
    /// </summary>
    /// <param name="role">The role name to check.</param>
    /// <returns><c>true</c> if the user is in the role; otherwise, <c>false</c>.</returns>
    bool IsInRole(string role);
}
