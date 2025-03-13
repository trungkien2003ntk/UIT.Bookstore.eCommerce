using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Emailing;
using KKBookstore.Domain.Emailing.TemplateModels;
using KKBookstore.Domain.Models;
using KKBookstore.Domain.Users;
using MediatR;

namespace KKBookstore.Application.Features.Authentication;

public record SendSignUpEmailCommand(string Email, string RedirectUrlBase) : IRequest<Result>;

public class SendSignUpEmailCommandHandler(
    IEmailService emailService,
    IIdentityService identityService,
    IApplicationDbContext dbContext
) : IRequestHandler<SendSignUpEmailCommand, Result>
{
    public async Task<Result> Handle(SendSignUpEmailCommand request, CancellationToken cancellationToken)
    {
        var recipientEmail = request.Email;
        var existingUser = await identityService.FindUserAsync(new(recipientEmail));
        if (existingUser.IsSuccess)
        {
            return Result.Failure(UserErrors.AlreadyExists);
        }

        // create a token, generate a link, and send an email
        var token = identityService.GenerateRegistrationToken(recipientEmail);
        var redirectUrl = $"{request.RedirectUrlBase}?token={token}";

        var emailModel = new AccountRegistrationEmailModel(redirectUrl);

        await emailService.SendAsync(
            recipientEmail,
            emailModel.Subject,
            emailModel
        );

        return Result.Success();
    }
}
