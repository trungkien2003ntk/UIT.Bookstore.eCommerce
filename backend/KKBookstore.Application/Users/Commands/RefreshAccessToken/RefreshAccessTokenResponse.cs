namespace KKBookstore.Application.Users.Commands.RefreshAccessToken;

public record RefreshAccessTokenResponse(string AccessToken, DateTime AccessTokenExpiration);
