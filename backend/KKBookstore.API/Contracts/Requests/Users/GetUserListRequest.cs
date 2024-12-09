using KKBookstore.API.HelperModels;
using KKBookstore.Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KKBookstore.API.Contracts.Requests.Users;

public class GetUserListRequest : IPaginatedQuery, ISortableQuery
{
    [ModelBinder(typeof(CommaDelimitedArrayModelBinder<int>))]
    public List<int>? UserIds { get; init; }
    public string SortBy { get; init; } = "CreationTime";
    public string SortDirection { get; init; } = "desc";
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
}
