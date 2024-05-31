using KKBookstore.Domain.Models;

namespace KKBookstore.Infrastructure.Identity
{
    public static class TokenErrors
    {
        public static readonly Error InvalidRefreshToken = Error.Unauthorized("Token.InvalidRefreshToken", "Invalid refresh token.");
    }
}
