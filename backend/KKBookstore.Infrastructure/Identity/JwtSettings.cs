namespace KKBookstore.Infrastructure.Identity;

public class JwtSettings
{
    public string Secret { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int AccessExpirationInMinutes { get; set; }
    public int RefreshExpirationInMonths { get; set; }
}
