using KKBookstore.Application.Common.Models;

namespace KKBookstore.Application.Features.Orders.Models;

public record OrderLineDto : BaseDto
{
    // move all from constructor down here as properties
    public int OrderId { get; init; }
    public int SkuId { get; init; }
    public string SkuOptionName { get; init; }
    public string ProductName { get; init; }
    public string ThumbnailUrl { get; init; }
    public decimal RecommendedRetailPrice { get; init; }
    public decimal UnitPrice { get; init; }
    public int Quantity { get; init; }
    public decimal Total => UnitPrice * Quantity;
}