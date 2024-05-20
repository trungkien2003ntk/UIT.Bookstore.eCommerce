using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Common;
using MediatR;

namespace KKBookstore.Application.Users.Commands.InitiateRegistration;

public class InitiateRegistrationCommandHandler(
    IOtpService otpService
) : IRequestHandler<InitiateRegistrationCommand, Result>
{
    public IOtpService OtpService { get; } = otpService;

    public async Task<Result> Handle(InitiateRegistrationCommand request, CancellationToken cancellationToken)
    {
        var otpResult = await OtpService.GenerateOtpAsync(request.Email);
        if (otpResult.IsFailure)
        {
            return Result.Failure(otpResult.Error);
        }

        var otp = otpResult.Value;
        var result = await OtpService.SendOtpAsync(request.Email, otp);

        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }        

        return Result.Success();
    }
}
