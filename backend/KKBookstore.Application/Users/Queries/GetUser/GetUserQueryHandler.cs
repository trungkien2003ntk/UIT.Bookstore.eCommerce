using AutoMapper;
using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Application.Users.Queries.Models;
using KKBookstore.Domain.Common;
using KKBookstore.Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Application.Users.Queries.GetUser;

public class GetUserQueryHandler(
    IApplicationDbContext dbContext,
    IMapper mapper
) : IRequestHandler<GetUserQuery, Result<UserDto>>
{
    public async Task<Result<UserDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
            .Where(u => u.IsActive)
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure<UserDto>(UserErrors.NotFound);
        }


        var userDto = mapper.Map<UserDto>(user);

        return Result.Success(userDto);

    }
}
