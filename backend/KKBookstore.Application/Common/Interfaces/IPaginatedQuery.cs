namespace KKBookstore.Common.Interfaces;

public interface IPaginatedQuery
{
    int PageNumber { get; init; }
    int PageSize { get; init; }
}
