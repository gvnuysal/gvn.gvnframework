using Gvn.GvnFramework.Domain.Repositories;
using Gvn.GvnFramework.EntityFramewokCore.Context;

namespace Gvn.GvnFramework.EntityFramewokCore.UnitOfWork;

/// <summary>
/// Entity Framework Core implementation of <see cref="IUnitOfWork"/>.
/// Delegates <see cref="SaveChangesAsync"/> to the underlying <see cref="GvnDbContext"/>,
/// which handles audit fields, soft deletes, and domain event dispatching.
/// </summary>
public class UnitOfWork(GvnDbContext context) : IUnitOfWork
{
    /// <inheritdoc />
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => context.SaveChangesAsync(cancellationToken);
}
