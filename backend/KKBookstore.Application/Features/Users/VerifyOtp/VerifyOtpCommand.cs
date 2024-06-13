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

        //var createTempCustomerResult = await identityService.CreateTemporaryCustomerAsync(request.Email);
        //if (createTempCustomerResult.IsFailure)
        //{
        //    return Result.Failure<VerifyOtpResponse>(createTempCustomerResult.Error);
        //}

        //// Return token
        //var generateTokenResult = await identityService.GenerateJwtToken(request.Email);
        //if (generateTokenResult.IsFailure)
        //{
        //    return Result.Failure<VerifyOtpResponse>(generateTokenResult.Error);
        //}

        //var response = new VerifyOtpResponse(
        //    generateTokenResult.Value.AccessToken,
        //    generateTokenResult.Value.AccessTokenExpiration,
        //    generateTokenResult.Value.RefreshToken
        //);

        return Result.Success();
    }
}