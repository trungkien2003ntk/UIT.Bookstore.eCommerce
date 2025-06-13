namespace KKBookstore.Infrastructure.Shipping;

/// <summary>
/// Configuration settings for GHN shipping service integration
/// </summary>
public class GhnConfiguration
{
    /// <summary>
    /// GHN API token for authentication
    /// </summary>
    public string Token { get; set; } = string.Empty;

    /// <summary>
    /// Default shop ID for orders
    /// </summary>
    public int ShopId { get; set; }

    /// <summary>
    /// GHN API base URL (production or test)
    /// </summary>
    public string BaseUrl { get; set; } = "https://online-gateway.ghn.vn/shiip/public-api/v2";

    /// <summary>
    /// Test environment base URL
    /// </summary>
    public string TestBaseUrl { get; set; } = "https://dev-online-gateway.ghn.vn/shiip/public-api/v2";

    /// <summary>
    /// Whether to use test environment
    /// </summary>
    public bool UseTestEnvironment { get; set; } = false;

    /// <summary>
    /// Request timeout in seconds
    /// </summary>
    public int TimeoutSeconds { get; set; } = 30;

    /// <summary>
    /// Default service type ID (2: E-commerce, 5: Traditional)
    /// </summary>
    public int DefaultServiceTypeId { get; set; } = 2;

    /// <summary>
    /// Default payment type ID (1: Shop pays, 2: Customer pays)
    /// </summary>
    public int DefaultPaymentTypeId { get; set; } = 1;

    /// <summary>
    /// Default required note for deliveries
    /// </summary>
    public string DefaultRequiredNote { get; set; } = "KHONGCHOXEMHANG";

    /// <summary>
    /// Gets the effective base URL based on environment setting
    /// </summary>
    public string EffectiveBaseUrl => UseTestEnvironment ? TestBaseUrl : BaseUrl;
}
