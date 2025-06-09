using KKBookstore.Common.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Cryptography;
using System.Text;

namespace KKBookstore.AI;

public class CachedRelatedProductsService(
    IMemoryCache memoryCache,
    IRelatedProductsService wrapee
) : RelatedProductsServiceBaseDecorator(wrapee)
{
    // Cache keys
    private readonly string _productIdCacheKey = "relatedproducts:productid";
    private readonly string _imageCacheKey = "relatedproducts:image";

    // Cache duration - AI calls are expensive, so cache for 4 hours
    private readonly TimeSpan _cacheDuration = TimeSpan.FromHours(4);

    private readonly IMemoryCache _memoryCache = memoryCache;

    public override async Task<List<int>> GetRelatedProductIdsByIdAsync(int productId, CancellationToken cancellationToken = default)
    {
        var key = $"{_productIdCacheKey}:{productId}";

        if (_memoryCache.TryGetValue(key, out List<int>? cachedProductIds))
        {
            return cachedProductIds!;
        }

        var result = await base.GetRelatedProductIdsByIdAsync(productId, cancellationToken);

        // Cache the result even if it's empty, to avoid repeated failed calls to expensive AI API
        _memoryCache.Set(key, result, _cacheDuration);

        return result;
    }

    public override async Task<List<int>> GetRelatedProductIdsByImageAsync(string base64Image, CancellationToken cancellationToken = default)
    {
        // Generate a hash of the base64 image to use as cache key since the image can be very long
        var imageHash = GenerateImageHash(base64Image);
        var key = $"{_imageCacheKey}:{imageHash}";

        if (_memoryCache.TryGetValue(key, out List<int>? cachedProductIds))
        {
            return cachedProductIds!;
        }

        var result = await base.GetRelatedProductIdsByImageAsync(base64Image, cancellationToken);

        // Cache the result even if it's empty, to avoid repeated failed calls to expensive AI API
        _memoryCache.Set(key, result, _cacheDuration);

        return result;
    }

    /// <summary>
    /// Generate a SHA256 hash of the base64 image to use as a cache key
    /// </summary>
    /// <param name="base64Image">The base64 image string</param>
    /// <returns>SHA256 hash of the image</returns>
    private static string GenerateImageHash(string base64Image)
    {
        using var sha256 = SHA256.Create();
        var imageBytes = Encoding.UTF8.GetBytes(base64Image);
        var hashBytes = sha256.ComputeHash(imageBytes);
        return Convert.ToHexString(hashBytes);
    }
}
