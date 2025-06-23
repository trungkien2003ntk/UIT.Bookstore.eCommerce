using KKBookstore.Common.Interfaces;
using KKBookstore.Common.Models.ResultDtos;
using KKBookstore.Users;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace KKBookstore.Geolocation;

public class CachedGeoCoordService : IGeoCoordService
{
    private readonly IMemoryCache _cache;
    private readonly IGeoCoordService _innerService;
    private readonly ILogger<CachedGeoCoordService> _logger;
    private readonly TimeSpan _cacheDuration;

    public CachedGeoCoordService(
        IMemoryCache cache,
        IGeoCoordService innerService,
        ILogger<CachedGeoCoordService> logger)
    {
        _cache = cache;
        _innerService = innerService;
        _logger = logger;
        _cacheDuration = TimeSpan.FromHours(24); // Cache for 24 hours
    }

    public async Task<GeoCoordResult> GetCoordinatesAsync(Address address, CancellationToken cancellationToken = default)
    {
        var cacheKey = GenerateCacheKey(address);

        if (_cache.TryGetValue(cacheKey, out GeoCoordResult? cachedResult) && cachedResult != null)
        {
            _logger.LogDebug("Cache hit for address geocoding: {CacheKey}", cacheKey);
            return cachedResult;
        }

        _logger.LogDebug("Cache miss for address geocoding: {CacheKey}", cacheKey);
        var result = await _innerService.GetCoordinatesAsync(address, cancellationToken);

        if (result.Success)
        {
            _cache.Set(cacheKey, result, _cacheDuration);
            _logger.LogDebug("Cached geocoding result for: {CacheKey}", cacheKey);
        }

        return result;
    }

    public async Task<GeoCoordResult> GetCoordinatesAsync(string fullAddress, CancellationToken cancellationToken = default)
    {
        var cacheKey = GenerateCacheKey(fullAddress);

        if (_cache.TryGetValue(cacheKey, out GeoCoordResult? cachedResult) && cachedResult != null)
        {
            _logger.LogDebug("Cache hit for string geocoding: {CacheKey}", cacheKey);
            return cachedResult;
        }

        _logger.LogDebug("Cache miss for string geocoding: {CacheKey}", cacheKey);
        var result = await _innerService.GetCoordinatesAsync(fullAddress, cancellationToken);

        if (result.Success)
        {
            _cache.Set(cacheKey, result, _cacheDuration);
            _logger.LogDebug("Cached geocoding result for: {CacheKey}", cacheKey);
        }

        return result;
    }

    private static string GenerateCacheKey(Address address)
    {
        return $"geocord_address_{address.ProvinceId}_{address.DistrictId}_{address.CommuneCode}_{address.DetailAddress?.GetHashCode()}";
    }

    private static string GenerateCacheKey(string fullAddress)
    {
        return $"geocord_string_{fullAddress.GetHashCode()}";
    }
}
