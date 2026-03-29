namespace Gvn.GvnFramework.Core.Exceptions;

/// <summary>
/// Exception thrown when a requested resource or entity cannot be found.
/// Maps to HTTP 404 in web applications.
/// </summary>
public class NotFoundException : GvnException
{
    /// <summary>
    /// Initializes a new instance of <see cref="NotFoundException"/> with a custom message.
    /// </summary>
    /// <param name="message">A human-readable description of what was not found.</param>
    public NotFoundException(string message) : base(message) { }

    /// <summary>
    /// Initializes a new instance of <see cref="NotFoundException"/> for a specific entity and key.
    /// </summary>
    /// <param name="entityName">The name of the entity type that was not found.</param>
    /// <param name="key">The identifier used to look up the entity.</param>
    public NotFoundException(string entityName, object key)
        : base($"{entityName} with key '{key}' was not found.") { }
}
