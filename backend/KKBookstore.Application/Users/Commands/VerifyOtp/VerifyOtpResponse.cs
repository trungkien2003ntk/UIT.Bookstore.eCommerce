namespace KKBookstore.Application.Users.Commands.VerifyOtp;

public record VerifyOtpResponse(string AccessToken, DateTime AccessTokenExpiration, string RefreshToken);
