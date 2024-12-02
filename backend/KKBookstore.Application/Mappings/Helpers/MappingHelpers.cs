using KKBookstore.Domain.Products;

namespace KKBookstore.Application.Mappings.Helpers;

internal static class MappingHelpers
{
    internal static string GetProductVariantThumbnailImageUrl(ProductVariant src)
    {
        if (src.ProductVariantOptionValues is null || src.ProductVariantOptionValues.Count == 0)
            return string.Empty;

        var thumbnailImageUrl = src.ProductVariantOptionValues
            .FirstOrDefault(sov => sov.Option.IsOptionWithImage)?.OptionValue?.ThumbnailImageUrl;

        return thumbnailImageUrl ?? "";
    }

    internal static string GetProductVariantLargeImageUrl(ProductVariant src)
    {
        var thumbnailImageUrl = src.ProductVariantOptionValues
            .FirstOrDefault(sov => sov.Option.IsOptionWithImage)?.OptionValue?.LargeImageUrl;

        return thumbnailImageUrl ?? "";
    }

    internal static string GetProductThumbnailImageUrl(Product src)
    {
        var firstProductImage = src.ProductImages.FirstOrDefault();
        return firstProductImage != null
            ? firstProductImage.ThumbnailImageUrl
            : GetProductVariantThumbnailImageUrl(src.ProductVariants.First());
    }

    public static string GetProductVariantOptionValuesString(ProductVariant productVariant)
    {
        if (productVariant.ProductVariantOptionValues is null || productVariant.ProductVariantOptionValues.Count == 0)
        {
            return string.Empty;
        }

        return string.Join(" / ", productVariant.ProductVariantOptionValues.Select(sov => sov.OptionValue.Value));
    }

    public static List<string> GetOptionValuesWithImages(ProductOption productOption)
    {
        var optionValueImageMap = productOption.Product.ProductVariants
            .SelectMany(pv => pv.ProductVariantOptionValues, (pv, sov) => new { pv=pv, sov })
            .Where(x => x.sov.OptionId == productOption.Id)
            .GroupBy(x => x.sov.OptionValueId)
            .ToDictionary(g => g.Key, g => g.First().pv);

        return productOption.OptionValues.Select(ov =>
        {
            if (optionValueImageMap.TryGetValue(ov.Id, out var productVariant))
            {
                return GetProductVariantThumbnailImageUrl(productVariant);
            }
            return "";
        }).ToList();
    }

    public static List<int> GetOptionIndices(ProductVariant productVariant, Product product)
    {
        var optionIndices = new List<int>();

        foreach (var sov in productVariant.ProductVariantOptionValues)
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
