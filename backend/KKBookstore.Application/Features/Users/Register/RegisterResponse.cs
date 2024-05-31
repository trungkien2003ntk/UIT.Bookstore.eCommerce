namespace KKBookstore.Application.Features.Users.Register;

public class RegisterResponse(string AccessToken, DateTime AccessTokenExpiration, string RefreshToken)
{
    public string AccessToken { get; init; } = AccessToken;
    public DateTime AccessTokenExpiration { get; init; } = AccessTokenExpiration;
    public string RefreshToken { get; init; } = RefreshToken;
}
