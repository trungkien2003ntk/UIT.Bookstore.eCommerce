namespace KKBookstore.Application.Features.Users.RefreshAccessToken;

public record RefreshAccessTokenResponse(string AccessToken, DateTime AccessTokenExpiration);
