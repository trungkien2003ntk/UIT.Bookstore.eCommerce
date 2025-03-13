using KKBookstore.Domain.Models;

namespace KKBookstore.Domain.Authentication;

public static class AuthErrors
{
    public static readonly Error NotFound = Error.NotFound("Auth.NotFound", "User was not found.");
    public static readonly Error Unknown = Error.Failure("Auth.Unknown", "An unknown error occurred.");
    public static readonly Error InvalidToken = Error.Unauthorized("Auth.InvalidToken", "Invalid token.");
}
