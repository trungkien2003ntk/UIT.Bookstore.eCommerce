using KKBookstore.Domain.Models;
using KKBookstore.Domain.Users;
using System.Security.Cryptography;

namespace KKBookstore.Infrastructure.Identity;

public class RefreshToken
{
    protected RefreshToken() { }

    private RefreshToken(
        string token,
        int userId,
        DateTimeOffset creationDate,
        DateTimeOffset expiryDate
    )
    {
        Token = token;
        UserId = userId;
        CreatedDate = creationDate;
        ExpiryDate = expiryDate;
    }

    public int Id { get; set; }
    public string Token { get; set; }
    public int UserId { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset ExpiryDate { get; set; }
    public User User { get; set; }

    public static Result<RefreshToken> Create(
        int userId,
        int refreshTokenExpirationInMonths
    )
    {
        var token = GenerateToken();
        var createdDate = DateTimeOffset.UtcNow;
        var expiryDate = DateTimeOffset.UtcNow.AddMonths(refreshTokenExpirationInMonths);

        return new RefreshToken(token, userId, createdDate, expiryDate);
    }

    private static string GenerateToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}
