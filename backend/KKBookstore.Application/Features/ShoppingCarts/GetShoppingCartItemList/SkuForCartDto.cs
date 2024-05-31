using KKBookstore.Application.Common.Models;

namespace KKBookstore.Application.Features.ShoppingCarts.GetShoppingCartItemList;

public record SkuForCartDto : BaseDto
{
    public int ProductId { get; init; }
    public string SkuValue { get; init; }
    public string SkuName { get; init; }
    public decimal UnitPrice { get; init; }
    public decimal RecommendedRetailPrice { get; init; }
    public int AvailableQuantity { get; init; }
    public int TotalQuantity { get; init; }
    public string Status { get; init; }
    public List<string> OptionNames { get; init; } = [];
    public List<int> OptionIndex { get; init; } = [];


    public SkuForCartDto PopulateIndex(List<ProductOptionAttributeDto> productOptions)
    {
        if (OptionNames.Count == 0)
        {
            return this;
        }

        var skuOptionValues = SkuName.Split(',').Select(sv => sv.Trim()).ToList();
        var optionValueDictionary = new Dictionary<string, string>();

        for (int i = 0; i < skuOptionValues.Count; i++)
        {
            optionValueDictionary.Add(OptionNames[i], skuOptionValues[i]);
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
