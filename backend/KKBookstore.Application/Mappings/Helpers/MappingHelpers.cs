using KKBookstore.Domain.Aggregates.ProductAggregate;

namespace KKBookstore.Application.Mappings.Helpers;

internal static class MappingHelpers
{
    internal static string GetSkuThumbnailImageUrl(Sku src)
    {
        var thumbnailImageUrl = src.SkuOptionValues
            .FirstOrDefault(sov => sov.Option.IsOptionWithImage)?.OptionValue?.ThumbnailImageUrl;

        return thumbnailImageUrl ?? "";
    }

    internal static string GetSkuLargeImageUrl(Sku src)
    {
        var thumbnailImageUrl = src.SkuOptionValues
            .FirstOrDefault(sov => sov.Option.IsOptionWithImage)?.OptionValue?.LargeImageUrl;

        return thumbnailImageUrl ?? "";
    }

    internal static string GetProductThumbnailImageUrl(Product src)
    {
        var firstProductImage = src.ProductImages.FirstOrDefault();
        return firstProductImage != null
            ? firstProductImage.ThumbnailImageUrl
            : GetSkuThumbnailImageUrl(src.Skus.First());
    }

    public static string GetSkuOptionValuesString(Sku sku)
    {
        return string.Join(" / ", sku.SkuOptionValues.Select(sov => sov.OptionValue.Value));
    }

    public static List<string> GetOptionValuesWithImages(ProductOption productOption)
    {
        var optionValueImageMap = productOption.Product.Skus
            .SelectMany(sku => sku.SkuOptionValues, (sku, sov) => new { sku, sov })
            .Where(x => x.sov.OptionId == productOption.Id)
            .GroupBy(x => x.sov.OptionValueId)
            .ToDictionary(g => g.Key, g => g.First().sku);

        return productOption.OptionValues.Select(ov =>
        {
            if (optionValueImageMap.TryGetValue(ov.Id, out var sku))
            {
                return GetSkuThumbnailImageUrl(sku);
            }
            return "";
        }).ToList();
    }

    public static List<int> GetOptionIndices(Sku sku, Product product)
    {
        var optionIndices = new List<int>();

        foreach (var sov in sku.SkuOptionValues)
        {
            var option = product.Options.FirstOrDefault(o => o.Id == sov.OptionId);
            if (option != null)
            {
                var listOptionValues = option.OptionValues.ToList();
                var index = listOptionValues.FindIndex(ov => ov.Id == sov.OptionValueId);
                if (index >= 0)
                {
                    optionIndices.Add(index);
                }
            }
        }

        return optionIndices;
    }
}
