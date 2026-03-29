using Gvn.GvnFramework.Domain.Repositories;
using Gvn.GvnFramework.EntityFramewokCore.Context;

namespace Gvn.GvnFramework.EntityFramewokCore.UnitOfWork;

public class UnitOfWork(GvnDbContext context) : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => context.SaveChangesAsync(cancellationToken);
}
