using KKBookstore.Domain.Aggregates.ProductAggregate;

namespace KKBookstore.Application.Common.Models;

public class SearchResult
{
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public List<int> ProductIds { get; set; } = [];
}
