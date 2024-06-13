namespace KKBookstore.Infrastructure.Shipping;

public class ShippingConfiguration
{
    public string BaseApiUrl { get; init; }
    public string Version { get; init; }
    public string BaseWard { get; init; }
    public string BaseDistrict { get; init; }
    public string BaseProvince { get; init; }
    public string BaseShippingFee { get; init; }
    public int ShopId { get; init; }
    public string Token { get; init; }
    public string BaseExpectedDeliveryTime { get; init; }
}
