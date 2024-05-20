namespace KKBookstore.Application.Users.Commands.SignIn;

public record SignInResponse(string AccessToken, DateTime AccessTokenExpiration, string RefreshToken);
