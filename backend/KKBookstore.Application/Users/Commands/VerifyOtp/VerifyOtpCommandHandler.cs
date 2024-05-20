using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Common;
using MediatR;

namespace KKBookstore.Application.Users.Commands.VerifyOtp;

public class VerifyOtpCommandHandler(
    IOtpService otpService,
    IIdentityService identityService
) : IRequestHandler<VerifyOtpCommand, Result<VerifyOtpResponse>>
{
    public async Task<Result<VerifyOtpResponse>> Handle(VerifyOtpCommand request, CancellationToken cancellationToken)
    {
        var verifyResult = otpService.VerifyOtp(request);

        if (verifyResult.IsFailure)
        {
            return Result.Failure<VerifyOtpResponse>(verifyResult.Error);
        }

        var createTempCustomerResult = await identityService.CreateTemporaryCustomerAsync(request.Email);
        if (createTempCustomerResult.IsFailure)
        {
            return Result.Failure<VerifyOtpResponse>(createTempCustomerResult.Error);
        }

        // Return token
        var generateTokenResult = await identityService.GenerateJwtToken(request.Email);
        if (generateTokenResult.IsFailure)
        {
            return Result.Failure<VerifyOtpResponse>(generateTokenResult.Error);
        }

        var response = new VerifyOtpResponse(
            generateTokenResult.Value.AccessToken,
            generateTokenResult.Value.AccessTokenExpiration,
            generateTokenResult.Value.RefreshToken
        );

        return response;
    }
}
