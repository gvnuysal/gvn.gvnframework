using Gvn.GvnFramework.Domain.Aggregates;
using Gvn.GvnFramework.Domain.Common;
using Gvn.GvnFramework.Domain.Entities;
using Gvn.GvnFramework.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gvn.GvnFramework.EntityFramewokCore.Context;

/// <summary>
/// Abstract base DbContext for the framework. Automatically handles audit field population,
/// soft-delete interception, and domain event dispatching on <see cref="SaveChangesAsync"/>.
/// Derive from this class for each bounded context's concrete DbContext.
/// </summary>
public abstract class GvnDbContext(DbContextOptions options, IMediator? mediator = null)
    : DbContext(options)
{
    /// <summary>
    /// Saves all pending changes to the database. Before saving, sets audit fields and
    /// converts hard deletes to soft deletes for <see cref="ISoftDeletable"/> entities.
    /// After saving, dispatches any domain events collected from aggregate roots.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The number of state entries written to the store.</returns>
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetAuditFields();
        SetSoftDeleteFields();

        var result = await base.SaveChangesAsync(cancellationToken);

        if (mediator is not null)
            await DispatchDomainEventsAsync(cancellationToken);

        return result;
    }

    private void SetAuditFields()
    {
        var entries = ChangeTracker.Entries<AuditableEntity>();
        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
                entry.Entity.SetCreated(null);
            else if (entry.State == EntityState.Modified)
                entry.Entity.SetUpdated(null);
        }
    }

    private void SetSoftDeleteFields()
    {
        var entries = ChangeTracker.Entries<Entity>()
            .Where(e => e.State == EntityState.Deleted && e.Entity is ISoftDeletable);

        foreach (var entry in entries)
        {
            entry.State = EntityState.Modified;
            var softDeletable = (ISoftDeletable)entry.Entity;
            entry.Property(nameof(ISoftDeletable.IsDeleted)).CurrentValue = true;
            entry.Property(nameof(ISoftDeletable.DeletedAt)).CurrentValue = DateTime.UtcNow;
        }
    }

    private async Task DispatchDomainEventsAsync(CancellationToken cancellationToken)
    {
        var aggregates = ChangeTracker.Entries<AggregateRoot>()
            .Where(e => e.Entity.DomainEvents.Count > 0)
            .Select(e => e.Entity)
            .ToList();

        var domainEvents = aggregates.SelectMany(a => a.DomainEvents).ToList();

        foreach (var aggregate in aggregates)
            aggregate.ClearDomainEvents();

        foreach (var domainEvent in domainEvents)
            await mediator!.Publish(domainEvent, cancellationToken);
    }

    /// <summary>
    /// Configures the model, applying global soft-delete query filters for all
    /// <see cref="ISoftDeletable"/> entity types.
    /// </summary>
    /// <param name="modelBuilder">The builder used to construct the model.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        ApplySoftDeleteFilter(modelBuilder);
    }

    private static void ApplySoftDeleteFilter(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (!typeof(ISoftDeletable).IsAssignableFrom(entityType.ClrType))
                continue;

            var method = typeof(GvnDbContext)
                .GetMethod(nameof(SetSoftDeleteQueryFilter),
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)!
                .MakeGenericMethod(entityType.ClrType);

            method.Invoke(null, [modelBuilder]);
        }
    }

    private static void SetSoftDeleteQueryFilter<T>(ModelBuilder modelBuilder)
        where T : Entity, ISoftDeletable
    {
        modelBuilder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
    }
}
