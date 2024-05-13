namespace KKBookstore.Application.Common.Interfaces;

public interface ISortableQuery
{
    string SortBy { get; init; }
    string SortDirection { get; init; }
}
