using KKBookstore.Application.Common.Interfaces;

namespace KKBookstore.Application.Common.Models.RequestDtos;

public record PagedAndSortedResultRequest : SortedResultRequest, IPaginatedQuery
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 12;
}