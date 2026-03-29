namespace Gvn.GvnFramework.Domain.Repositories;

/// <summary>
/// Represents the Unit of Work pattern, providing a single transactional boundary
/// across multiple repository operations.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Persists all pending changes to the underlying data store within a single transaction.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The number of state entries written to the store.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
