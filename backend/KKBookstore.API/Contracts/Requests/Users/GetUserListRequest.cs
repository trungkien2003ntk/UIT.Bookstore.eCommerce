using KKBookstore.Common.Interfaces;
using KKBookstore.HelperModels;
using Microsoft.AspNetCore.Mvc;

namespace KKBookstore.Contracts.Requests.Users;

public class GetUserListRequest : IPaginatedQuery, ISortableQuery
{
    [ModelBinder(typeof(CommaDelimitedArrayModelBinder<int>))]
    public List<int>? UserIds { get; init; }
    public string SortBy { get; init; } = "CreationTime";
    public string SortDirection { get; init; } = "desc";
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
}
