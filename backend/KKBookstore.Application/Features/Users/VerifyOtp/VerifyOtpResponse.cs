namespace KKBookstore.Application.Features.Users.VerifyOtp;

public record VerifyOtpResponse(string AccessToken, DateTime AccessTokenExpiration, string RefreshToken);
