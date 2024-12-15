using KKBookstore.Application.Common.Models.ResultDtos;

namespace KKBookstore.Application.Features.ShoppingCarts.GetShoppingCartItemList;

public record GetShoppingCartResponse
{
    public List<ShoppingCartItemDto> Items { get; init; } = [];

    public GetShoppingCartResponse()
    {
    }

    public sealed record ShoppingCartItemDto : BaseFullAuditedDto
    {
        public int ProductId { get; init; }
        public int ProductVariantId { get; init; }
        public string ProductVariantName { get; init; } = null!;
        public string ProductName { get; init; } = null!;
        public int ProductTypeId { get; init; }
        public decimal UnitPrice { get; init; }
        public decimal RecommendedRetailPrice { get; init; }
        public decimal TotalPrice => UnitPrice * Quantity;
        public decimal BasicDiscountRate { get; init; }
        public int Quantity { get; init; }
        // todo: Currently, the available quantity is the same as total quantity
        // think about implement this later
        public int AvailableQuantity { get; init; }
        public int TotalQuantity { get; init; }
        public string ImageUrl { get; init; } = null!;
        public string Description { get; init; } = null!;
        public List<ProductVariantForCartDto> ProductVariantVariations { get; init; } = [];
        public List<ProductOptionAttributeDto> ProductOptions { get; init; } = [];

        public sealed record ProductVariantForCartDto : BaseDto
        {
            public int ProductId { get; init; }
            public string SkuValue { get; init; } = null!;
            public string ProductVariantName { get; init; } = null!;
            public decimal UnitPrice { get; init; }
            public decimal RecommendedRetailPrice { get; init; }
            public int AvailableQuantity { get; init; }
            public int TotalQuantity { get; init; }
            public List<string> OptionNames { get; init; } = [];
            public List<int> OptionIndex { get; init; } = [];


            public ProductVariantForCartDto PopulateIndex(List<ProductOptionAttributeDto> productOptions)
            {
                if (OptionNames.Count == 0)
                {
                    return this;
                }

                var productVariantOptionValues = ProductVariantName.Split(',').Select(sv => sv.Trim()).ToList();
                var optionValueDictionary = new Dictionary<string, string>();

                if (productVariantOptionValues.Count > OptionNames.Count)
                {
                    // if the SkuName contain comma in the name, but not separator for multiple options

                }

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

        public record ProductOptionAttributeDto
        {
            public string Name { get; init; } = null!;
            public IEnumerable<string> Values { get; init; } = [];
            public IEnumerable<string> Images { get; init; } = [];
        }
    }
}
