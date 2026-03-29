namespace Gvn.GvnFramework.Domain.ValueObjects;

/// <summary>
/// Base class for DDD value objects. Equality is determined by the values of
/// the object's components, not by identity.
/// </summary>
public abstract class ValueObject : IEquatable<ValueObject>
{
    /// <summary>
    /// Returns the components used to determine equality for this value object.
    /// Derived classes must enumerate all significant fields or properties.
    /// </summary>
    /// <returns>An ordered sequence of the value object's equality components.</returns>
    protected abstract IEnumerable<object?> GetEqualityComponents();

    /// <summary>
    /// Determines whether this value object is equal to another by comparing their components.
    /// </summary>
    /// <param name="other">The other value object to compare with.</param>
    /// <returns><c>true</c> if all equality components are equal; otherwise, <c>false</c>.</returns>
    public bool Equals(ValueObject? other)
    {
        if (other is null) return false;
        if (GetType() != other.GetType()) return false;
        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    /// <inheritdoc />
    public override bool Equals(object? obj) => Equals(obj as ValueObject);

    /// <inheritdoc />
    public override int GetHashCode()
        => GetEqualityComponents()
            .Select(x => x?.GetHashCode() ?? 0)
            .Aggregate((x, y) => x ^ y);

    /// <summary>Returns <c>true</c> if the two value objects are equal.</summary>
    public static bool operator ==(ValueObject? left, ValueObject? right) => Equals(left, right);

    /// <summary>Returns <c>true</c> if the two value objects are not equal.</summary>
    public static bool operator !=(ValueObject? left, ValueObject? right) => !Equals(left, right);
}
