using System.Linq.Expressions;
using Gvn.GvnFramework.Domain.Entities;

namespace Gvn.GvnFramework.EntityFramewokCore.Specifications;

/// <summary>
/// Abstract base class for the Specification Pattern. Encapsulates query criteria, eager-load
/// includes, ordering, and pagination so that query logic can be expressed as reusable objects.
/// </summary>
/// <typeparam name="T">The entity type the specification applies to.</typeparam>
public abstract class Specification<T> where T : Entity
{
    /// <summary>Gets the filter expression applied to the query, or <c>null</c> for no filtering.</summary>
    public Expression<Func<T, bool>>? Criteria { get; protected set; }

    /// <summary>Gets the list of navigation properties to eagerly load.</summary>
    public List<Expression<Func<T, object>>> Includes { get; } = [];

    /// <summary>Gets the ascending-order expression, or <c>null</c> if no order is applied.</summary>
    public Expression<Func<T, object>>? OrderBy { get; protected set; }

    /// <summary>Gets the descending-order expression, or <c>null</c> if no order is applied.</summary>
    public Expression<Func<T, object>>? OrderByDescending { get; protected set; }

    /// <summary>Gets the maximum number of records to return, or <c>null</c> if paging is not enabled.</summary>
    public int? Take { get; protected set; }

    /// <summary>Gets the number of records to skip, or <c>null</c> if paging is not enabled.</summary>
    public int? Skip { get; protected set; }

    /// <summary>Gets a value indicating whether paging is enabled (i.e. <see cref="Take"/> has a value).</summary>
    public bool IsPagingEnabled => Take.HasValue;

    /// <summary>Registers a navigation property to be eagerly loaded with the query.</summary>
    /// <param name="includeExpression">A lambda selecting the navigation property to include.</param>
    protected void AddInclude(Expression<Func<T, object>> includeExpression)
        => Includes.Add(includeExpression);

    /// <summary>Applies skip/take paging to the specification.</summary>
    /// <param name="skip">The number of records to skip.</param>
    /// <param name="take">The maximum number of records to return.</param>
    protected void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
    }

    /// <summary>Sets an ascending sort on the specified property.</summary>
    /// <param name="orderByExpression">A lambda selecting the property to sort by.</param>
    protected void ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
        => OrderBy = orderByExpression;

    /// <summary>Sets a descending sort on the specified property.</summary>
    /// <param name="orderByDescExpression">A lambda selecting the property to sort by.</param>
    protected void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
        => OrderByDescending = orderByDescExpression;
}
