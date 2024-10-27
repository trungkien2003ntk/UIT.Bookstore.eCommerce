using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Aggregates.UserAggregate;
using KKBookstore.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Application.Features.Users.GetUser;

public record GetUserQuery(int UserId) : IRequest<Result<GetUserResponse>>;

public class GetUserQueryHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<GetUserQuery, Result<GetUserResponse>>
{
    public async Task<Result<GetUserResponse>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
            .Where(u => u.IsActive)
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure<GetUserResponse>(UserErrors.NotFound);
        }

        var userRoles = await (
                from userRole in dbContext.UserRoles
                join role in dbContext.Roles on userRole.RoleId equals role.Id
                where userRole.UserId == user.Id
                select role.Name
            ).ToListAsync(cancellationToken);

        var userDto = new GetUserResponse
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email!,
            PhoneNumber = user.PhoneNumber,
            DateOfBirth = user.DateOfBirth,
            FullName = user.FullName,
            Status = user.Status.ToString(),
            ImageUrl = user.ImageUrl,
            Roles = userRoles
        };

        return Result.Success(userDto);
    }
}
