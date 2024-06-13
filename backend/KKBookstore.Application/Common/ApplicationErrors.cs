using KKBookstore.Domain.Models;

namespace KKBookstore.Application.Common;

public static class ApplicationErrors
{
    public static readonly Error OtpSentFailed = Error.Failure("Application.OtpSentFailed", "OTP sent failed");
    public static readonly Error OtpIncorrect = Error.Unauthorized("Application.OtpIncorrect", "OTP is incorrect");
    public static readonly Error OtpExpired = Error.Forbidden("Application.OtpExpired", "OTP is expired");
}
