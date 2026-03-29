namespace Gvn.GvnFramework.Domain.Common;

/// <summary>
/// Contract for entities that support soft deletion — marking a record as deleted
/// without physically removing it from the data store.
/// </summary>
public interface ISoftDeletable
{
    /// <summary>Gets a value indicating whether this entity has been soft-deleted.</summary>
    bool IsDeleted { get; }

    /// <summary>Gets the UTC date and time when the entity was soft-deleted, or <c>null</c> if not deleted.</summary>
    DateTime? DeletedAt { get; }

    /// <summary>Gets the identifier of the user who deleted the entity, or <c>null</c> if unknown.</summary>
    string? DeletedBy { get; }
}
