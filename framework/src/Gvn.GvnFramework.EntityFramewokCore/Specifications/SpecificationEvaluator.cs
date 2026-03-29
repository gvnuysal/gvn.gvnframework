using Gvn.GvnFramework.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gvn.GvnFramework.EntityFramewokCore.Specifications;

public static class SpecificationEvaluator
{
    public static IQueryable<T> GetQuery<T>(IQueryable<T> inputQuery, Specification<T> specification)
        where T : Entity
    {
        var query = inputQuery;

        if (specification.Criteria is not null)
            query = query.Where(specification.Criteria);

        query = specification.Includes
            .Aggregate(query, (current, include) => current.Include(include));

        if (specification.OrderBy is not null)
            query = query.OrderBy(specification.OrderBy);
        else if (specification.OrderByDescending is not null)
            query = query.OrderByDescending(specification.OrderByDescending);

        if (specification.IsPagingEnabled)
            query = query.Skip(specification.Skip!.Value).Take(specification.Take!.Value);

        return query;
    }
}
