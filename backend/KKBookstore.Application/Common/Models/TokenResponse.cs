namespace KKBookstore.Application.Common.Models;

public class TokenResponse(string AccessToken, DateTime AccessTokenExpiration, string RefreshToken)
{
    public string AccessToken { get; init; } = AccessToken;
    public DateTime AccessTokenExpiration { get; init; } = AccessTokenExpiration;
    public string RefreshToken { get; init; } = RefreshToken;
}