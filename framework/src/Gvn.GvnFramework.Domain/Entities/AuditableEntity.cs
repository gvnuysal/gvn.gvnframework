using Gvn.GvnFramework.Domain.Common;

namespace Gvn.GvnFramework.Domain.Entities;

public abstract class AuditableEntity : Entity, IAuditable
{
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public string? CreatedBy { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public string? UpdatedBy { get; private set; }

    public void SetCreated(string? createdBy)
    {
        CreatedAt = DateTime.UtcNow;
        CreatedBy = createdBy;
    }

    public void SetUpdated(string? updatedBy)
    {
        UpdatedAt = DateTime.UtcNow;
        UpdatedBy = updatedBy;
    }
}
