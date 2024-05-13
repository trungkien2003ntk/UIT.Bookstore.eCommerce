namespace KKBookstore.Application.Common.Models;

public class PaginatedResult<T>(
    List<T> items,
    int totalCount,
    int pageSize,
    int pageNumber)
{
    public List<T> Items { get; set; } = items;
    public int TotalCount { get; set; } = totalCount;
    public int PageSize { get; set; } = pageSize;
    public int PageNumber { get; set; } = pageNumber;
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}
