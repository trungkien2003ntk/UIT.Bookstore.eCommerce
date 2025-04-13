using KKBookstore.Common.Interfaces;
using KKBookstore.Models;
using KKBookstore.Users;
using MediatR;

namespace KKBookstore.Features.Users.UpdateUser;

public record UpdateUserCommand(
    int Id,
    string FullName,
    string Email,
    string PhoneNumber,
    DateTimeOffset DateOfBirth,
    Gender Gender,
    string Status,
    string? ImageUrl
) : IRequest<Result>;

public class UpdateUserCommandHandler(
    IIdentityService identityService
) : IRequestHandler<UpdateUserCommand, Result>
{
    public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var replaceResult = await identityService.UpdateUserAsync(request);

        if (replaceResult.IsFailure)
        {
            return Result.Failure(replaceResult.Error);
        }

        return replaceResult;
    }
}
