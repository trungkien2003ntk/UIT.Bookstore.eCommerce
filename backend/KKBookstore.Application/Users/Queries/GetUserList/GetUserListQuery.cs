using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Application.Common.Models;
using KKBookstore.Application.Users.Queries.Models;
using KKBookstore.Domain.Common;
using MediatR;

namespace KKBookstore.Application.Users.Queries.GetUserList;

public record GetUserListQuery()
    : IRequest<Result<PaginatedResult<UserDto>>>, IPaginatedQuery, ISortableQuery
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
    public string SortBy { get; init; } = "CreatedWhen";
    public string SortDirection { get; init; } = "desc";

    // filter

}
