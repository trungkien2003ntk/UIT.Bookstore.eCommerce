using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Models;
using MediatR;

namespace KKBookstore.Application.Features.Users.RequestPasswordReset;

public record RequestPasswordResetCommand(string Email) : IRequest<Result>;

public class RequestPasswordResetCommandHandler(
    IIdentityService identityService,
    IEmailService emailService
) : IRequestHandler<RequestPasswordResetCommand, Result>
{
    private readonly IIdentityService _identityService = identityService;
    private readonly IEmailService _emailService = emailService;

    public async Task<Result> Handle(RequestPasswordResetCommand request, CancellationToken cancellationToken)
    {
        var result = await _identityService.GenerateResetPasswordTokenAsync(request.Email);

        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        return Result.Success();
    }
}