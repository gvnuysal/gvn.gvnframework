namespace Gvn.GvnFramework.Application.Common;

/// <summary>
/// Wraps a page of query results together with pagination metadata.
/// </summary>
/// <typeparam name="T">The type of items in the page.</typeparam>
public class PagedResult<T>
{
    /// <summary>Gets the items on the current page.</summary>
    public IReadOnlyList<T> Items { get; }

    /// <summary>Gets the total number of items across all pages.</summary>
    public int TotalCount { get; }

    /// <summary>Gets the 1-based current page number.</summary>
    public int PageNumber { get; }

    /// <summary>Gets the maximum number of items per page.</summary>
    public int PageSize { get; }

    /// <summary>Gets the total number of pages based on <see cref="TotalCount"/> and <see cref="PageSize"/>.</summary>
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

    /// <summary>Gets a value indicating whether a previous page exists.</summary>
    public bool HasPreviousPage => PageNumber > 1;

    /// <summary>Gets a value indicating whether a next page exists.</summary>
    public bool HasNextPage => PageNumber < TotalPages;

    /// <summary>
    /// Initializes a new paged result with the supplied items and pagination metadata.
    /// </summary>
    /// <param name="items">The items on the current page.</param>
    /// <param name="totalCount">The total number of items across all pages.</param>
    /// <param name="pageNumber">The 1-based current page number.</param>
    /// <param name="pageSize">The maximum number of items per page.</param>
    public PagedResult(IEnumerable<T> items, int totalCount, int pageNumber, int pageSize)
    {
        Items = items.ToList().AsReadOnly();
        TotalCount = totalCount;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }

    /// <summary>
    /// Creates an empty paged result with no items and a total count of zero.
    /// </summary>
    /// <param name="pageNumber">The 1-based current page number. Defaults to <c>1</c>.</param>
    /// <param name="pageSize">The maximum number of items per page. Defaults to <c>10</c>.</param>
    /// <returns>An empty <see cref="PagedResult{T}"/>.</returns>
    public static PagedResult<T> Empty(int pageNumber = 1, int pageSize = 10)
        => new([], 0, pageNumber, pageSize);
}
