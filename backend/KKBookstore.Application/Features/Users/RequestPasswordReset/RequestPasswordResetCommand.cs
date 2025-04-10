using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Emailing;
using KKBookstore.Domain.Emailing.TemplateModels;
using KKBookstore.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace KKBookstore.Application.Features.Users.RequestPasswordReset;

public record RequestPasswordResetCommand(string Email, string RedirectUrlBase) : IRequest<Result>;

public class RequestPasswordResetCommandHandler(
    IIdentityService identityService,
    IEmailService emailService,
    ILogger<RequestPasswordResetCommandHandler> logger
) : IRequestHandler<RequestPasswordResetCommand, Result>
{

    public async Task<Result> Handle(RequestPasswordResetCommand request, CancellationToken cancellationToken)
    {
        var result = await identityService.GenerateResetPasswordTokenAsync(request.Email);
        var userResult = await identityService.FindUserAsync(new(request.Email));


        if (result.IsFailure || userResult.IsFailure)
        {
            // just log this out, we still return Success for security reasons
            logger.LogWarning("Failed to generate reset password token for {Email}. Reason: {Error}", request.Email, result.Error);
            return Result.Success();
        }

        var token = result.Value;
        var user = userResult.Value;
        var redirectUrl = $"{request.RedirectUrlBase}/{user.Id}?token={token}";

        var emailModel = new ForgotPasswordEmailModel(user.FullName, redirectUrl);
        await emailService.SendAsync(
            request.Email,
            emailModel.Subject,
            emailModel);

        return Result.Success();
    }
}