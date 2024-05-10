using KKBookstore.Domain.Common;
using KKBookstore.Domain.Users;
using System.Security.Cryptography;

namespace KKBookstore.Infrastructure.Identity;

public class RefreshToken
{
    private RefreshToken(
        string token,
        string jwtId,
        string userId,
        DateTimeOffset creationDate,
        DateTimeOffset expiryDate
    )
    {
        Token = token;
        JwtId = jwtId;
        UserId = userId;
        CreationDate = creationDate;
        ExpiryDate = expiryDate;
        Used = false;
        Invalidated = false;
    }
    public int Id { get; set; }
    public string Token { get; set; }
    public string JwtId { get; set; }
    public string UserId { get; set; }
    public DateTimeOffset CreationDate { get; set; }
    public DateTimeOffset ExpiryDate { get; set; }
    public bool Used { get; set; }
    public bool Invalidated { get; set; }
    public User User { get; set; }

    public static Result<RefreshToken> Create(
        string userId,
        int refreshTokenExpirationInMonths
    )
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Result.Failure<RefreshToken>(Error.NullValue);
        }
        var jwtId = Guid.NewGuid().ToString();
        var token = GenerateToken();
        var creationDate = DateTimeOffset.UtcNow;
        var expiryDate = DateTimeOffset.UtcNow.AddMonths(refreshTokenExpirationInMonths);

        return new RefreshToken(token, jwtId, userId, creationDate, expiryDate);
    }

    private static string GenerateToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}
