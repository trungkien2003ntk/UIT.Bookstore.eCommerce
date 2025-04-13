using KKBookstore.Models;

namespace KKBookstore.Identity;

public static class TokenErrors
{
    public static readonly Error InvalidRefreshToken = Error.Unauthorized("Token.InvalidRefreshToken", "Invalid refresh token.");
}
