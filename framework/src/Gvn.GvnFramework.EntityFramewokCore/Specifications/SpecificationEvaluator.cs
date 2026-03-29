using Gvn.GvnFramework.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gvn.GvnFramework.EntityFramewokCore.Specifications;

/// <summary>
/// Translates a <see cref="Specification{T}"/> into an EF Core <see cref="IQueryable{T}"/>
/// by applying criteria, eager-load includes, ordering, and paging.
/// </summary>
public static class SpecificationEvaluator
{
    /// <summary>
    /// Builds a queryable from a base query and a specification, applying all
    /// criteria, includes, ordering, and paging defined by the specification.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    /// <param name="inputQuery">The base queryable to apply the specification to.</param>
    /// <param name="specification">The specification that defines the query shape.</param>
    /// <returns>An <see cref="IQueryable{T}"/> with all specification settings applied.</returns>
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
