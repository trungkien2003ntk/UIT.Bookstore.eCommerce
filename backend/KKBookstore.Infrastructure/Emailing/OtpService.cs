using KKBookstore.Application.Common;
using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Application.Features.Users.VerifyOtp;
using KKBookstore.Domain.Emailing;
using KKBookstore.Domain.Models;
using Microsoft.Extensions.Caching.Memory;

namespace KKBookstore.Infrastructure.Emailing;
public class OtpService(
    IMemoryCache cache,
    IEmailSender emailService,
    IIdentityService identityService
) : IOtpService
{
    private readonly IMemoryCache _cache = cache;
    private readonly IEmailSender _emailService = emailService;

    public Result<string> GenerateOtp(string emailAddress)
    {
        var otp = new Random().Next(100000, 999999).ToString();

        // Store the OTP in cache for 3 minutes
        _cache.Set(emailAddress, otp, TimeSpan.FromMinutes(3));

        return otp;
    }

    public async Task<Result> SendOtpAsync(string emailAddress, string otp)
    {
        try
        {
            await _emailService.SendOtp(emailAddress, otp);
        }
        catch
        {
            return Result.Failure(ApplicationErrors.OtpSentFailed);
        }

        return Result.Success();
    }

    public Result<bool> VerifyOtp(VerifyOtpCommand request)
    {
        if (_cache.TryGetValue(request.Email, out string? existingOtp))
        {
            if (!string.IsNullOrEmpty(existingOtp) && existingOtp == request.Otp)
            {
                _cache.Remove(request.Email);

                return Result.Success(true);
            }
            else
            {
                // OTP is incorrect
                return Result.Failure<bool>(ApplicationErrors.OtpIncorrect);
            }
        }

        // OTP is expired
        return Result.Failure<bool>(ApplicationErrors.OtpExpired);
    }
}
