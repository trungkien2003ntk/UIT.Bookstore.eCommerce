using AutoMapper;
using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Application.Common.Models.ResultDtos;
using KKBookstore.Application.Extensions;
using KKBookstore.Domain.Models;
using KKBookstore.Domain.Users;
using MediatR;

namespace KKBookstore.Application.Features.Users.GetUserList;

public record GetUserListQuery()
    : IRequest<Result<PagedResult<GetUserListResponse>>>, IPaginatedQuery, ISortableQuery
{
    public List<int>? UserIds { get; init; }
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
    public string SortBy { get; init; } = "CreationTime";
    public string SortDirection { get; init; } = "desc";

    // filter

}

public class GetUserListHandler(
    IApplicationDbContext dbContext,
    IMapper mapper
) : IRequestHandler<GetUserListQuery, Result<PagedResult<GetUserListResponse>>>
{
    public async Task<Result<PagedResult<GetUserListResponse>>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
    {
        IQueryable<User> query = dbContext.Users;

        var validSortProperties = new List<string>
        {
            nameof(User.Id),
            nameof(User.FirstName),
            nameof(User.LastName),
            nameof(User.Email),
            nameof(User.PhoneNumber),
            nameof(User.CreationTime)
        };

        string sortProperty = request.SortBy;

        if (request.UserIds is not null && request.UserIds.Count > 0)
            query = query.Where(x => request.UserIds.Contains(x.Id));

        PagedResult<User> paginatedUsers;

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
            return Result.Failure<PagedResult<GetUserListResponse>>(Error.InvalidSortProperty(sortProperty, string.Join(',', validSortProperties)));
        }

        var mappedPaginatedUsers = mapper.Map<PagedResult<GetUserListResponse>>(paginatedUsers);

        return Result.Success(mappedPaginatedUsers);
    }
}
