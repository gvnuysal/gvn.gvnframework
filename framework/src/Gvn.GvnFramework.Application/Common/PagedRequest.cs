namespace Gvn.GvnFramework.Application.Common;

/// <summary>
/// Base class for paginated query requests. Encapsulates page number, page size, and sort parameters.
/// </summary>
public class PagedRequest
{
    private int _pageSize = 10;

    /// <summary>
    /// Gets or sets the 1-based page number. Defaults to <c>1</c>.
    /// </summary>
    public int PageNumber { get; set; } = 1;

    /// <summary>
    /// Gets or sets the number of items per page. Clamped between <c>1</c> and <c>100</c>. Defaults to <c>10</c>.
    /// </summary>
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > 100 ? 100 : value < 1 ? 1 : value;
    }

    /// <summary>
    /// Gets or sets the name of the property to sort by, or <c>null</c> for default ordering.
    /// </summary>
    public string? SortBy { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the sort order is descending. Defaults to <c>false</c>.
    /// </summary>
    public bool SortDescending { get; set; } = false;

    /// <summary>
    /// Gets the number of items to skip, calculated from <see cref="PageNumber"/> and <see cref="PageSize"/>.
    /// </summary>
    public int Skip => (PageNumber - 1) * PageSize;
}
