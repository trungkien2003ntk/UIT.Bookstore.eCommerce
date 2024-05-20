using KKBookstore.Application.Users.Commands.VerifyOtp;
using KKBookstore.Domain.Common;

namespace KKBookstore.Application.Common.Interfaces;

public interface IOtpService
{
    Result<bool> VerifyOtp(VerifyOtpCommand request);
    Task<Result> SendOtpAsync(string email, string otp);
    Task<Result<string>> GenerateOtpAsync(string email);
}
