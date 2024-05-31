using AutoMapper;
using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Application.Features.Users.GetUserList;
using KKBookstore.Domain.Aggregates.UserAggregate;
using KKBookstore.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Application.Features.Users.GetUser;

public record GetUserQuery(int UserId) : IRequest<Result<GetUserListResponse>>;

public class GetUserQueryHandler(
    IApplicationDbContext dbContext,
    IMapper mapper
) : IRequestHandler<GetUserQuery, Result<GetUserListResponse>>
{
    public async Task<Result<GetUserListResponse>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
            .Where(u => u.IsActive)
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure<GetUserListResponse>(UserErrors.NotFound);
        }


        var userDto = mapper.Map<GetUserListResponse>(user);

        return Result.Success(userDto);
    }
}
