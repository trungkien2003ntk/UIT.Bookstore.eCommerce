using KKBookstore.Application.Common.Models;

namespace KKBookstore.Application.Features.ShoppingCarts.UpdateShoppingCartItem;

public record UpdateShoppingCartResponse
{
    public List<ShoppingCartItemDto> UpdatedItems { get; init; } = [];
    public decimal TotalPrice { get; init; }
    public DiscountDetailDto DiscountDetail { get; init; }


    public sealed record ShoppingCartItemDto : BaseDto
    {
        public int ProductId { get; init; }
        public int ProductVariantId { get; init; }
        public bool IsSelected { get; init; }
        public string ProductVariantName { get; init; }
        public string ProductName { get; init; }
        public int ProductTypeId { get; init; }
        public decimal UnitPrice { get; init; }
        public decimal RecommendedRetailPrice { get; init; }
        public decimal TotalPrice => UnitPrice * Quantity;
        public int Quantity { get; init; }
        // todo: Currently, the available quantity is the same as total quantity
        // think about implement this later
        public int AvailableQuantity { get; init; }
        public int TotalQuantity { get; init; }
        public string ImageUrl { get; init; }
        public string Description { get; init; }
        public DateTimeOffset CreatedWhen { get; init; }
        public List<ProductVariantForCartDto> ProductVariantVariations { get; init; }
        public List<ProductOptionAttributeDto> ProductOptions { get; init; }

        public sealed record ProductVariantForCartDto : BaseDto
        {
            public int ProductId { get; init; }
            public string SkuValue { get; init; }
            public string ProductVariantName { get; init; }
            public decimal UnitPrice { get; init; }
            public decimal RecommendedRetailPrice { get; init; }
            public decimal BasicDiscountRate { get; init; }
            public int AvailableQuantity { get; init; }
            public int TotalQuantity { get; init; }
            public string Status { get; init; }
            public List<string> OptionNames { get; init; } = [];
            public List<int> OptionIndex { get; init; } = [];


            public ProductVariantForCartDto PopulateIndex(List<ProductOptionAttributeDto> productOptions)
            {
                Console.WriteLine(OptionNames);
                if (OptionNames.Count == 0)
                {
                    return this;
                }

                var productVariantOptionValues = ProductVariantName.Split(',').Select(sv => sv.Trim()).ToList();
                var optionValueDictionary = new Dictionary<string, string>();

                for (int i = 0; i < productVariantOptionValues.Count; i++)
                {
                    optionValueDictionary.Add(OptionNames[i], productVariantOptionValues[i]);
                }

                foreach (var (optionName, value) in optionValueDictionary)
                {
                    var index = productOptions
                        .FirstOrDefault(po => po.Name == optionName)!.Values.ToList().IndexOf(value);

                    if (index != -1)
                    {
                        OptionIndex.Add(index);
                        break;
                    }
                }

                return this;
            }
        }

        public sealed record ProductOptionAttributeDto
        {
            public string Name { get; init; }
            public IEnumerable<string> Values { get; init; }
            public IEnumerable<string> Images { get; init; }
        }
    }
    public sealed record DiscountDetailDto
    {
        public decimal Subtotal { get; set; }
        public decimal TotalSaved { get; set; }
        public decimal Total { get; set; }
        public IEnumerable<DiscountForReason> Breakdown { get; set; }

        public sealed class DiscountForReason
        {
            public string DiscountName { get; set; }
            public decimal DiscountValue { get; set; }
        }
    }
}