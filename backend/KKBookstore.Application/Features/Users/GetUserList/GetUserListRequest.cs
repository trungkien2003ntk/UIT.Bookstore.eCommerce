using AutoMapper;
using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Application.Common.Models;
using KKBookstore.Application.Extensions;
using KKBookstore.Domain.Aggregates.UserAggregate;
using KKBookstore.Domain.Models;
using MediatR;

namespace KKBookstore.Application.Features.Users.GetUserList;

public record GetUserListRequest()
    : IRequest<Result<PaginatedResult<GetUserListResponse>>>, IPaginatedQuery, ISortableQuery
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
    public string SortBy { get; init; } = "CreatedWhen";
    public string SortDirection { get; init; } = "desc";

    // filter

}

public class GetUserListHandler(
    IApplicationDbContext dbContext,
    IMapper mapper
) : IRequestHandler<GetUserListRequest, Result<PaginatedResult<GetUserListResponse>>>
{
    public async Task<Result<PaginatedResult<GetUserListResponse>>> Handle(GetUserListRequest request, CancellationToken cancellationToken)
    {
        IQueryable<User> query = dbContext.Users;

        var validSortProperties = new List<string> { "Id", "FirstName", "LastName", "Email", "PhoneNumber" };
        string sortProperty = request.SortBy;

        PaginatedResult<User> paginatedUsers;

        try
        {
            paginatedUsers = await query.SortAndPaginateAsync(
                sortProperty,
                request.SortDirection,
                validSortProperties,
                request.PageNumber,
                request.PageSize,
                cancellationToken);
        }
        catch
        {
            return Result.Failure<PaginatedResult<GetUserListResponse>>(Error.InvalidSortProperty(sortProperty, string.Join(',', validSortProperties)));
        }

        var mappedPaginatedUsers = mapper.Map<PaginatedResult<GetUserListResponse>>(paginatedUsers);

        return Result.Success(mappedPaginatedUsers);
    }
}
