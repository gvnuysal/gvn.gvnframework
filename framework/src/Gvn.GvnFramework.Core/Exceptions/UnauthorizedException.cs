namespace Gvn.GvnFramework.Core.Exceptions;

/// <summary>
/// Exception thrown when a caller attempts to perform an operation they are not authorized to execute.
/// Maps to HTTP 401 in web applications.
/// </summary>
public class UnauthorizedException : GvnException
{
    /// <summary>
    /// Initializes a new instance of <see cref="UnauthorizedException"/> with the default message.
    /// </summary>
    public UnauthorizedException() : base("You are not authorized to perform this action.") { }

    /// <summary>
    /// Initializes a new instance of <see cref="UnauthorizedException"/> with a custom message.
    /// </summary>
    /// <param name="message">A human-readable description of why the action is unauthorized.</param>
    public UnauthorizedException(string message) : base(message) { }
}
