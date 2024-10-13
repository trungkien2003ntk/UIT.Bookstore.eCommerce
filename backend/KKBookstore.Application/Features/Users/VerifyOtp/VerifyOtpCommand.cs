using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Models;
using MediatR;

namespace KKBookstore.Application.Features.Users.VerifyOtp;

public record VerifyOtpCommand(string Email, string Otp) : IRequest<Result>;

public class VerifyOtpCommandHandler(
    IOtpService otpService,
    IIdentityService identityService
) : IRequestHandler<VerifyOtpCommand, Result>
{
    public async Task<Result> Handle(VerifyOtpCommand request, CancellationToken cancellationToken)
    {
        var verifyResult = otpService.VerifyOtp(request);

        if (verifyResult.IsFailure)
        {
            return Result.Failure(verifyResult.Error);
        }

        return Result.Success();
    }
}