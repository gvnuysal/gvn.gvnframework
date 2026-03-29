namespace Gvn.GvnFramework.Core.Exceptions;

/// <summary>
/// Exception thrown when one or more validation rules are violated.
/// Carries a dictionary of field-level validation errors.
/// Maps to HTTP 422 or 400 in web applications.
/// </summary>
public class ValidationException : GvnException
{
    /// <summary>
    /// Gets the validation errors keyed by field name, each containing one or more error messages.
    /// </summary>
    public IReadOnlyDictionary<string, string[]> Errors { get; }

    /// <summary>
    /// Initializes a new instance of <see cref="ValidationException"/> with a simple message and no field errors.
    /// </summary>
    /// <param name="message">A human-readable description of the validation failure.</param>
    public ValidationException(string message) : base(message)
    {
        Errors = new Dictionary<string, string[]>();
    }

    /// <summary>
    /// Initializes a new instance of <see cref="ValidationException"/> with field-level validation errors.
    /// </summary>
    /// <param name="errors">
    /// A dictionary mapping each field name to an array of validation error messages for that field.
    /// </param>
    public ValidationException(IDictionary<string, string[]> errors)
        : base("One or more validation errors occurred.")
    {
        Errors = errors.AsReadOnly();
    }
}
