namespace Gvn.GvnFramework.Domain.Events;

/// <summary>
/// Marker interface for all domain events.
/// Domain events represent significant state changes that have occurred within an aggregate.
/// </summary>
public interface IDomainEvent
{
    /// <summary>Gets the unique identifier of this event instance.</summary>
    Guid EventId { get; }

    /// <summary>Gets the UTC date and time when the event occurred.</summary>
    DateTime OccurredOn { get; }
}
