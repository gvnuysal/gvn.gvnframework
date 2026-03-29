namespace Gvn.GvnFramework.Application.Common;

public class PagedRequest
{
    private int _pageSize = 10;

    public int PageNumber { get; set; } = 1;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > 100 ? 100 : value < 1 ? 1 : value;
    }

    public string? SortBy { get; set; }
    public bool SortDescending { get; set; } = false;

    public int Skip => (PageNumber - 1) * PageSize;
}
