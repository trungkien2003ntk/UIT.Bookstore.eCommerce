namespace KKBookstore.Domain.Users;

public class SignInResponse(
    string AccessToken,
    DateTime AccessTokenExpiration,
    string RefreshToken
)
{
    public string AccessToken { get; set; } = AccessToken;
    public DateTime AccessTokenExpiration { get; set; } = AccessTokenExpiration;
    public string RefreshToken { get; set; } = RefreshToken;
}
