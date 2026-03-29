namespace Gvn.GvnFramework.Domain.Events;

/// <summary>
/// Abstract base record for all domain events. Provides default implementations of
/// <see cref="IDomainEvent.EventId"/> and <see cref="IDomainEvent.OccurredOn"/>.
/// </summary>
public abstract record DomainEvent : IDomainEvent
{
    /// <summary>Gets the unique identifier automatically assigned to this event instance.</summary>
    public Guid EventId { get; } = Guid.NewGuid();

    /// <summary>Gets the UTC timestamp recorded when the event was created.</summary>
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}
