namespace Gvn.GvnFramework.Domain.Common;

/// <summary>
/// Contract for entities that track when and by whom they were created and last modified.
/// </summary>
public interface IAuditable
{
    /// <summary>Gets the UTC date and time when the entity was created.</summary>
    DateTime CreatedAt { get; }

    /// <summary>Gets the identifier of the user who created the entity, or <c>null</c> if unknown.</summary>
    string? CreatedBy { get; }

    /// <summary>Gets the UTC date and time of the last modification, or <c>null</c> if never modified.</summary>
    DateTime? UpdatedAt { get; }

    /// <summary>Gets the identifier of the user who last modified the entity, or <c>null</c> if not modified.</summary>
    string? UpdatedBy { get; }
}
