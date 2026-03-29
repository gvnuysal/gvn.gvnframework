using Gvn.GvnFramework.Domain.Repositories;
using Gvn.GvnFramework.EntityFramewokCore.Context;
using Microsoft.EntityFrameworkCore;

namespace Gvn.GvnFramework.EntityFramewokCore.UnitOfWork;

/// <summary>
/// Entity Framework Core implementation of <see cref="IUnitOfWork"/>.
/// Delegates <see cref="SaveChangesAsync"/> to the underlying <see cref="DbContext"/>,
/// which handles audit fields, soft deletes, and domain event dispatching.
/// </summary>
/// <typeparam name="TContext">The concrete <see cref="DbContext"/> type derived from <see cref="GvnDbContext{TContext}"/>.</typeparam>
public class UnitOfWork<TContext>(TContext context) : IUnitOfWork
    where TContext : DbContext
{
    /// <inheritdoc />
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => context.SaveChangesAsync(cancellationToken);
}
