using KKBookstore.Application.Features.Users.VerifyOtp;
using KKBookstore.Domain.Models;

namespace KKBookstore.Application.Common.Interfaces;

public interface IOtpService
{
    Result<bool> VerifyOtp(VerifyOtpCommand request);
    Task<Result> SendOtpAsync(string emailAddress, string otp);
    Result<string> GenerateOtp(string emailAddress);
}
