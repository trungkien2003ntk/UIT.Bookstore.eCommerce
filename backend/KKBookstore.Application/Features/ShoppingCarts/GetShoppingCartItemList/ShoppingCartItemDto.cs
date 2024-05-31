using KKBookstore.Application.Common.Models;

namespace KKBookstore.Application.Features.ShoppingCarts.GetShoppingCartItemList;

public record ShoppingCartItemDto : BaseDto
{
    public int ProductId { get; init; }
    public int SkuId { get; init; }
    public string SkuName { get; init; }
    public string ProductName { get; init; }
    public int ProductTypeId { get; init; }
    public decimal UnitPrice { get; init; }
    public decimal RecommendedRetailPrice { get; init; }
    public int Quantity { get; init; }
    // todo: Currently, the available quantity is the same as total quantity
    // think about implement this later
    public int AvailableQuantity { get; init; }
    public int TotalQuantity { get; init; }
    public string ImageUrl { get; init; }
    public string Description { get; init; }
    public DateTimeOffset CreatedWhen { get; init; }
    public List<SkuForCartDto> SkuVariations { get; init; }
    public List<ProductOptionAttributeDto> ProductOptions { get; init; }
}
