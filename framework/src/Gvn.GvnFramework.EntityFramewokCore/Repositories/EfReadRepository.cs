using System.Linq.Expressions;
using Gvn.GvnFramework.Domain.Entities;
using Gvn.GvnFramework.Domain.Repositories;
using Gvn.GvnFramework.EntityFramewokCore.Context;
using Gvn.GvnFramework.EntityFramewokCore.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Gvn.GvnFramework.EntityFramewokCore.Repositories;

/// <summary>
/// Entity Framework Core implementation of <see cref="IReadRepository{T}"/> using
/// no-tracking queries for read-only operations.
/// </summary>
/// <typeparam name="T">The entity type, which must derive from <see cref="Entity"/>.</typeparam>
/// <typeparam name="TContext">The concrete <see cref="DbContext"/> type derived from <see cref="GvnDbContext{TContext}"/>.</typeparam>
public class EfReadRepository<T, TContext>(TContext context) : IReadRepository<T>
    where T : Entity
    where TContext : DbContext
{
    /// <summary>A no-tracking queryable over the entity set.</summary>
    protected readonly IQueryable<T> Query = context.Set<T>().AsNoTracking();

    /// <inheritdoc />
    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await Query.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

    /// <inheritdoc />
    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        => await Query.ToListAsync(cancellationToken);

    /// <inheritdoc />
    public async Task<IEnumerable<T>> FindAsync(
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
        => await Query.Where(predicate).ToListAsync(cancellationToken);

    /// <inheritdoc />
    public async Task<bool> AnyAsync(
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
        => await Query.AnyAsync(predicate, cancellationToken);

    /// <inheritdoc />
    public async Task<int> CountAsync(
        Expression<Func<T, bool>>? predicate = null,
        CancellationToken cancellationToken = default)
        => predicate is null
            ? await Query.CountAsync(cancellationToken)
            : await Query.CountAsync(predicate, cancellationToken);

    /// <summary>
    /// Retrieves all entities matching the given specification, applying criteria, includes, ordering, and paging.
    /// </summary>
    /// <param name="specification">The specification that defines the query criteria and options.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The entities matching the specification.</returns>
    public async Task<IEnumerable<T>> GetBySpecAsync(
        Specification<T> specification,
        CancellationToken cancellationToken = default)
        => await SpecificationEvaluator.GetQuery(Query, specification).ToListAsync(cancellationToken);
}
