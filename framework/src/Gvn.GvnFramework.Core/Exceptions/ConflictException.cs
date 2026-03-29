namespace Gvn.GvnFramework.Core.Exceptions;

/// <summary>
/// Exception thrown when an operation conflicts with the current state of a resource,
/// such as attempting to create a duplicate entity.
/// Maps to HTTP 409 in web applications.
/// </summary>
public class ConflictException : GvnException
{
    /// <summary>
    /// Initializes a new instance of <see cref="ConflictException"/> with a custom message.
    /// </summary>
    /// <param name="message">A human-readable description of the conflict.</param>
    public ConflictException(string message) : base(message) { }

    /// <summary>
    /// Initializes a new instance of <see cref="ConflictException"/> for a specific entity and key.
    /// </summary>
    /// <param name="entityName">The name of the entity type that caused the conflict.</param>
    /// <param name="key">The identifier of the conflicting entity.</param>
    public ConflictException(string entityName, object key)
        : base($"{entityName} with key '{key}' already exists.") { }
}
