namespace Gvn.GvnFramework.Core.Exceptions;

/// <summary>
/// Base exception class for all framework-specific exceptions.
/// All domain and application exceptions should derive from this class.
/// </summary>
public class GvnException : Exception
{
    /// <summary>
    /// Initializes a new instance of <see cref="GvnException"/> with the given message.
    /// </summary>
    /// <param name="message">A human-readable description of the error.</param>
    public GvnException(string message) : base(message) { }

    /// <summary>
    /// Initializes a new instance of <see cref="GvnException"/> with the given message and inner exception.
    /// </summary>
    /// <param name="message">A human-readable description of the error.</param>
    /// <param name="innerException">The exception that caused this exception.</param>
    public GvnException(string message, Exception innerException) : base(message, innerException) { }
}
