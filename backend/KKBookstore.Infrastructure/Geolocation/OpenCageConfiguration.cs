namespace KKBookstore.Infrastructure.Geolocation;

public class OpenCageConfiguration
{
    public string ApiKey { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = "https://api.opencagedata.com";
    public string Language { get; set; } = "en";
    public int TimeoutSeconds { get; set; } = 30;
    public int MaxRetries { get; set; } = 3;
}
