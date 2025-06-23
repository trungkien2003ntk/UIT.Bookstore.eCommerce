using System.ComponentModel.DataAnnotations;

namespace KKBookstore.Identity;

public class BlacklistedToken
{
    protected BlacklistedToken() { }

    private BlacklistedToken(string token, DateTimeOffset expireAt)
    {
        Token = token;
        ExpireAt = expireAt;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(1000)]
    public string Token { get; set; } = string.Empty;

    [Required]
    public DateTimeOffset ExpireAt { get; set; }

    [Required]
    public DateTimeOffset CreatedAt { get; set; }

    public static BlacklistedToken Create(string token, DateTimeOffset expireAt)
    {
        return new BlacklistedToken(token, expireAt);
    }
}
