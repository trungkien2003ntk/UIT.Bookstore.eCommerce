﻿using KKBookstore.Application.Common;
using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Application.Features.Users.VerifyOtp;
using KKBookstore.Domain.Models;
using Microsoft.Extensions.Caching.Memory;

namespace KKBookstore.Infrastructure.Email;
public class OtpService(
    IMemoryCache cache,
    IEmailService emailService,
    IIdentityService identityService
) : IOtpService
{
    private readonly IMemoryCache _cache = cache;
    private readonly IEmailService _emailService = emailService;

    public Result<string> GenerateOtp(string email)
    {
        var otp = new Random().Next(100000, 999999).ToString();

        // Store the OTP in cache for 3 minutes
        _cache.Set(email, otp, TimeSpan.FromMinutes(3));

        return otp;
    }

    public async Task<Result> SendOtpAsync(string email, string otp)
    {
        try
        {
            await _emailService.SendOtp(email, otp);
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
                // OTP is correct, remove it from cache
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
