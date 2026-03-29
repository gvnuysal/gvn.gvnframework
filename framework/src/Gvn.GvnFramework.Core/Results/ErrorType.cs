namespace Gvn.GvnFramework.Core.Results;

/// <summary>
/// Represents the category of an error that occurred during an operation.
/// </summary>
public enum ErrorType
{
    /// <summary>A general or unclassified failure.</summary>
    Failure = 0,

    /// <summary>One or more validation rules were violated.</summary>
    Validation = 1,

    /// <summary>The requested resource could not be found.</summary>
    NotFound = 2,

    /// <summary>A conflict occurred, such as a duplicate resource.</summary>
    Conflict = 3,

    /// <summary>The caller is not authorized to perform the operation.</summary>
    Unauthorized = 4
}
