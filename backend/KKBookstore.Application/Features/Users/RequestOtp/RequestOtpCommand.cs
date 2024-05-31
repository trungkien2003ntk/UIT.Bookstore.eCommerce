using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Models;
using MediatR;

namespace KKBookstore.Application.Features.Users.RequestOtp;

public record RequestOtpCommand(string Email) : IRequest<Result>;

public class RequestOtpCommandHandler(
    IOtpService otpService
) : IRequestHandler<RequestOtpCommand, Result>
{
    public IOtpService OtpService { get; } = otpService;

    public async Task<Result> Handle(RequestOtpCommand request, CancellationToken cancellationToken)
    {
        var otpResult = OtpService.GenerateOtp(request.Email);
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
