namespace Gvn.GvnFramework.Core.Results;

/// <summary>
/// Represents an error that occurred during an operation, carrying a code, human-readable message,
/// and a categorized <see cref="ErrorType"/>.
/// </summary>
/// <param name="Code">A short machine-readable identifier for the error (e.g. "USER_NOT_FOUND").</param>
/// <param name="Message">A human-readable description of the error.</param>
/// <param name="Type">The category of the error. Defaults to <see cref="ErrorType.Failure"/>.</param>
public sealed record Error(string Code, string Message, ErrorType Type = ErrorType.Failure)
{
    /// <summary>
    /// Represents the absence of an error. Used as a sentinel value for successful results.
    /// </summary>
    public static readonly Error None = new(string.Empty, string.Empty);

    /// <summary>
    /// Creates a validation error.
    /// </summary>
    /// <param name="code">A short identifier for the validation rule that was violated.</param>
    /// <param name="message">A description of the validation failure.</param>
    /// <returns>An <see cref="Error"/> with <see cref="ErrorType.Validation"/>.</returns>
    public static Error Validation(string code, string message)
        => new(code, message, ErrorType.Validation);

    /// <summary>
    /// Creates a not-found error.
    /// </summary>
    /// <param name="code">A short identifier for the missing resource.</param>
    /// <param name="message">A description of what was not found.</param>
    /// <returns>An <see cref="Error"/> with <see cref="ErrorType.NotFound"/>.</returns>
    public static Error NotFound(string code, string message)
        => new(code, message, ErrorType.NotFound);

    /// <summary>
    /// Creates a conflict error.
    /// </summary>
    /// <param name="code">A short identifier for the conflicting resource.</param>
    /// <param name="message">A description of the conflict.</param>
    /// <returns>An <see cref="Error"/> with <see cref="ErrorType.Conflict"/>.</returns>
    public static Error Conflict(string code, string message)
        => new(code, message, ErrorType.Conflict);

    /// <summary>
    /// Creates an unauthorized error.
    /// </summary>
    /// <param name="code">A short identifier for the authorization failure.</param>
    /// <param name="message">A description of why the operation is unauthorized.</param>
    /// <returns>An <see cref="Error"/> with <see cref="ErrorType.Unauthorized"/>.</returns>
    public static Error Unauthorized(string code, string message)
        => new(code, message, ErrorType.Unauthorized);

    /// <summary>
    /// Creates a general failure error.
    /// </summary>
    /// <param name="code">A short identifier for the failure.</param>
    /// <param name="message">A description of what failed.</param>
    /// <returns>An <see cref="Error"/> with <see cref="ErrorType.Failure"/>.</returns>
    public static Error Failure(string code, string message)
        => new(code, message, ErrorType.Failure);
}
