namespace KKBookstore.Application.Common.Models.ResultDtos;

public class SearchResult
{
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public List<int> ProductIds { get; set; } = [];
}
