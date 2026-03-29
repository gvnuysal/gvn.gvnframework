using System.Linq.Expressions;
using Gvn.GvnFramework.Domain.Entities;

namespace Gvn.GvnFramework.Domain.Repositories;

/// <summary>
/// Read-only repository interface for querying domain entities without modifying them.
/// Use this in query handlers to enforce CQRS read/write separation.
/// </summary>
/// <typeparam name="T">The entity type, which must derive from <see cref="Entity"/>.</typeparam>
public interface IReadRepository<T> where T : Entity
{
    /// <summary>
    /// Retrieves an entity by its unique identifier.
    /// </summary>
    /// <param name="id">The entity's unique identifier.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The entity if found; otherwise, <c>null</c>.</returns>
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all entities of type <typeparamref name="T"/>.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>All entities in the store.</returns>
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns all entities that satisfy the given predicate.
    /// </summary>
    /// <param name="predicate">A LINQ expression to filter entities.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>All matching entities.</returns>
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Determines whether any entity satisfies the given predicate.
    /// </summary>
    /// <param name="predicate">A LINQ expression to test entities.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns><c>true</c> if at least one entity matches; otherwise, <c>false</c>.</returns>
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Counts the number of entities that satisfy the given predicate.
    /// </summary>
    /// <param name="predicate">An optional filter expression. If <c>null</c>, counts all entities.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The number of matching entities.</returns>
    Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken cancellationToken = default);
}
