using Gvn.GvnFramework.Domain.Common;

namespace Gvn.GvnFramework.Domain.Entities;

/// <summary>
/// Base class for entities that track creation and modification metadata.
/// Implements <see cref="IAuditable"/> and extends <see cref="Entity"/>.
/// </summary>
public abstract class AuditableEntity : Entity, IAuditable
{
    /// <summary>Gets the UTC date and time when the entity was created.</summary>
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    /// <summary>Gets the identifier of the user who created the entity, or <c>null</c> if unknown.</summary>
    public string? CreatedBy { get; private set; }

    /// <summary>Gets the UTC date and time of the last update, or <c>null</c> if the entity has never been updated.</summary>
    public DateTime? UpdatedAt { get; private set; }

    /// <summary>Gets the identifier of the user who last updated the entity, or <c>null</c> if not updated.</summary>
    public string? UpdatedBy { get; private set; }

    /// <summary>
    /// Sets the creation timestamp and the creator identifier.
    /// </summary>
    /// <param name="createdBy">The identifier of the user creating the entity.</param>
    public void SetCreated(string? createdBy)
    {
        CreatedAt = DateTime.UtcNow;
        CreatedBy = createdBy;
    }

    /// <summary>
    /// Sets the last-updated timestamp and the modifier identifier.
    /// </summary>
    /// <param name="updatedBy">The identifier of the user updating the entity.</param>
    public void SetUpdated(string? updatedBy)
    {
        UpdatedAt = DateTime.UtcNow;
        UpdatedBy = updatedBy;
    }
}
