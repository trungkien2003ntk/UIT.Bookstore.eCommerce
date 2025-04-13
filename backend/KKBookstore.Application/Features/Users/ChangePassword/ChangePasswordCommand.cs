using KKBookstore.Common.Interfaces;
using KKBookstore.Models;
using MediatR;

namespace KKBookstore.Features.Users.ChangePassword;

public record ChangePasswordCommand(int UserId, string Email, string CurrentPassword, string NewPassword) : IRequest<Result>;

public class ChangePasswordCommandHandler(
    IIdentityService identityService
) : IRequestHandler<ChangePasswordCommand, Result>
{
    private readonly IIdentityService _identityService = identityService;

    public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var result = await _identityService.ChangePasswordAsync(request);

        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        return Result.Success();
    }
}
