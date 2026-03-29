using Gvn.GvnFramework.Domain.Entities;
using Gvn.GvnFramework.Domain.Repositories;
using Gvn.GvnFramework.EntityFramewokCore.Context;
using Microsoft.EntityFrameworkCore;

namespace Gvn.GvnFramework.EntityFramewokCore.Repositories;

/// <summary>
/// Entity Framework Core implementation of <see cref="IRepository{T}"/> providing
/// full CRUD operations for a domain entity.
/// </summary>
/// <typeparam name="T">The entity type, which must derive from <see cref="Entity"/>.</typeparam>
/// <typeparam name="TContext">The concrete <see cref="DbContext"/> type derived from <see cref="GvnDbContext{TContext}"/>.</typeparam>
public class EfRepository<T, TContext>(TContext context) : IRepository<T>
    where T : Entity
    where TContext : DbContext
{
    /// <summary>The EF Core <see cref="DbSet{T}"/> used to interact with the entity table.</summary>
    protected readonly DbSet<T> DbSet = context.Set<T>();

    /// <inheritdoc />
    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await DbSet.FindAsync([id], cancellationToken);

    /// <inheritdoc />
    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        => await DbSet.ToListAsync(cancellationToken);

    /// <inheritdoc />
    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        => await DbSet.AddAsync(entity, cancellationToken);

    /// <inheritdoc />
    public Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        DbSet.Update(entity);
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        DbSet.Remove(entity);
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        => await DbSet.AnyAsync(e => e.Id == id, cancellationToken);
}
