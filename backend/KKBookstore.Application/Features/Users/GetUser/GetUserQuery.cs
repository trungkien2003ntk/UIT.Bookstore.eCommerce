using AutoMapper;
using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Application.Features.Users.GetUserList;
using KKBookstore.Domain.Aggregates.UserAggregate;
using KKBookstore.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Application.Features.Users.GetUser;

public record GetUserQuery(int UserId) : IRequest<Result<GetUserResponse>>;

public class GetUserQueryHandler(
    IApplicationDbContext dbContext,
    IMapper mapper
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


        var userDto = new GetUserResponse
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            DateOfBirth = user.DateOfBirth,
            FullName = user.FullName,
            Status = user.Status.ToString(),
            ImageUrl = user.ImageUrl
        };

        return Result.Success(userDto);
    }
}
