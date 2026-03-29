using Gvn.GvnFramework.Domain.Aggregates;
using Gvn.GvnFramework.Domain.Common;
using Gvn.GvnFramework.Domain.Entities;
using Gvn.GvnFramework.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gvn.GvnFramework.EntityFramewokCore.Context;

public abstract class GvnDbContext(DbContextOptions options, IMediator? mediator = null)
    : DbContext(options)
{
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
