using KKBookstore.Application.Common.Interfaces;

namespace KKBookstore.Application.Common.Models.RequestDtos;

public record SortedResultRequest : ISortableQuery
{
    public string SortBy { get; init; } // Field to sort by
    public string SortDirection { get; init; } = "ASC"; // ASC or DESC, default is ASC
}