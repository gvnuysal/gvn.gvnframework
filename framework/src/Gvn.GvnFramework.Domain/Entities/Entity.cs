namespace Gvn.GvnFramework.Domain.Entities;

/// <summary>
/// Base class for all domain entities. Provides identity-based equality using a <see cref="Guid"/> identifier.
/// </summary>
public abstract class Entity : IEquatable<Entity>
{
    /// <summary>
    /// Gets the unique identifier of this entity. Defaults to a newly generated <see cref="Guid"/>.
    /// </summary>
    public Guid Id { get; protected set; } = Guid.NewGuid();

    /// <summary>
    /// Determines whether this entity is equal to another entity by comparing their identifiers.
    /// </summary>
    /// <param name="other">The other entity to compare with.</param>
    /// <returns><c>true</c> if both entities share the same <see cref="Id"/>; otherwise, <c>false</c>.</returns>
    public bool Equals(Entity? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj) => Equals(obj as Entity);

    /// <inheritdoc />
    public override int GetHashCode() => Id.GetHashCode();

    /// <summary>
    /// Returns <c>true</c> if the two entities have the same identity.
    /// </summary>
    public static bool operator ==(Entity? left, Entity? right) => Equals(left, right);

    /// <summary>
    /// Returns <c>true</c> if the two entities have different identities.
    /// </summary>
    public static bool operator !=(Entity? left, Entity? right) => !Equals(left, right);
}
