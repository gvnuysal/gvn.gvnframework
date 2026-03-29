using Gvn.GvnFramework.Domain.Entities;
using Gvn.GvnFramework.Domain.Events;

namespace Gvn.GvnFramework.Domain.Aggregates;

/// <summary>
/// Base class for DDD aggregate roots. Manages a list of domain events that are raised
/// during state transitions and dispatched after the aggregate is persisted.
/// </summary>
public abstract class AggregateRoot : AuditableEntity
{
    private readonly List<IDomainEvent> _domainEvents = [];

    /// <summary>
    /// Gets the domain events that have been raised on this aggregate and not yet dispatched.
    /// </summary>
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    /// <summary>
    /// Adds a domain event to the pending events list.
    /// Call this inside aggregate methods when meaningful state transitions occur.
    /// </summary>
    /// <param name="domainEvent">The domain event to enqueue.</param>
    protected void AddDomainEvent(IDomainEvent domainEvent)
        => _domainEvents.Add(domainEvent);

    /// <summary>
    /// Removes all pending domain events from this aggregate.
    /// Typically called by the infrastructure layer after events have been dispatched.
    /// </summary>
    public void ClearDomainEvents()
        => _domainEvents.Clear();
}
