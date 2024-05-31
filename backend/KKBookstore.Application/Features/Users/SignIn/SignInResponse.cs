namespace KKBookstore.Application.Features.Users.SignIn;

public record SignInResponse(string AccessToken, DateTime AccessTokenExpiration, string RefreshToken);
