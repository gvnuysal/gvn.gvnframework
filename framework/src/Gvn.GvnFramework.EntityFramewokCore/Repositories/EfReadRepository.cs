using System.Linq.Expressions;
using Gvn.GvnFramework.Domain.Entities;
using Gvn.GvnFramework.Domain.Repositories;
using Gvn.GvnFramework.EntityFramewokCore.Context;
using Gvn.GvnFramework.EntityFramewokCore.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Gvn.GvnFramework.EntityFramewokCore.Repositories;

public class EfReadRepository<T>(GvnDbContext context) : IReadRepository<T>
    where T : Entity
{
    protected readonly IQueryable<T> Query = context.Set<T>().AsNoTracking();

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await Query.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        => await Query.ToListAsync(cancellationToken);

    public async Task<IEnumerable<T>> FindAsync(
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
        => await Query.Where(predicate).ToListAsync(cancellationToken);

    public async Task<bool> AnyAsync(
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
        => await Query.AnyAsync(predicate, cancellationToken);

    public async Task<int> CountAsync(
        Expression<Func<T, bool>>? predicate = null,
        CancellationToken cancellationToken = default)
        => predicate is null
            ? await Query.CountAsync(cancellationToken)
            : await Query.CountAsync(predicate, cancellationToken);

    public async Task<IEnumerable<T>> GetBySpecAsync(
        Specification<T> specification,
        CancellationToken cancellationToken = default)
        => await SpecificationEvaluator.GetQuery(Query, specification).ToListAsync(cancellationToken);
}
