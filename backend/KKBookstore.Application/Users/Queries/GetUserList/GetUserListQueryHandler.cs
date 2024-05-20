using AutoMapper;
using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Application.Common.Models;
using KKBookstore.Application.Extensions;
using KKBookstore.Application.Users.Queries.Models;
using KKBookstore.Domain.Common;
using KKBookstore.Domain.Users;
using MediatR;

namespace KKBookstore.Application.Users.Queries.GetUserList;

public class GetUserListQueryHandler(
    IApplicationDbContext dbContext,
    IMapper mapper
) : IRequestHandler<GetUserListQuery, Result<PaginatedResult<UserDto>>>
{
    public async Task<Result<PaginatedResult<UserDto>>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
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
            return Result.Failure<PaginatedResult<UserDto>>(Error.InvalidSortProperty(sortProperty, string.Join(',', validSortProperties)));
        }

        var mappedPaginatedUsers = mapper.Map<PaginatedResult<UserDto>>(paginatedUsers);

        return Result.Success(mappedPaginatedUsers);
    }
}
