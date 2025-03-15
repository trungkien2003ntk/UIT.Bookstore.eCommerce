using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace KKBookstore.Application.Features.Users.ResetPassword;

public record ResetPasswordCommand(string Email, string Token, string NewPassword) : IRequest<Result>;

public class ResetPasswordCommandHandler(
    IIdentityService identityService,
    ILogger<ResetPasswordCommandHandler> logger
) : IRequestHandler<ResetPasswordCommand, Result>
{
    private readonly IIdentityService _identityService = identityService;

    public async Task<Result> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        // TODO: Implement a cooldown mechanism to prevent brute force attacks

        var result = await _identityService.ResetPasswordAsync(request.Email, request.Token, request.NewPassword);

        if (result.IsFailure)
        {
            logger.LogError("Failed to reset password for {Email}. Reason: {Error}", request.Email, result.Error);
            return Result.Failure(result.Error);
        }

        return Result.Success();
    }
}
