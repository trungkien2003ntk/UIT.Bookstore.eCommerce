using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Models;
using KKBookstore.Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KKBookstore.Features.Users.ResetPassword;

public record ResetPasswordCommand(int UserId, string Token, string NewPassword) : IRequest<Result>;

public class ResetPasswordCommandHandler(
    IIdentityService identityService,
    IApplicationDbContext dbContext,
    ILogger<ResetPasswordCommandHandler> logger
) : IRequestHandler<ResetPasswordCommand, Result>
{
    private readonly IIdentityService _identityService = identityService;

    public async Task<Result> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        // TODO: Implement a cooldown mechanism to prevent brute force attacks
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure(UserErrors.NotFound);
        }

        var result = await _identityService.ResetPasswordAsync(user.Email!, request.Token, request.NewPassword);

        if (result.IsFailure)
        {
            logger.LogError("Failed to reset password for {Email}. Reason: {Error}", user.Email!, result.Error);
            return Result.Failure(result.Error);
        }

        return Result.Success();
    }
}
