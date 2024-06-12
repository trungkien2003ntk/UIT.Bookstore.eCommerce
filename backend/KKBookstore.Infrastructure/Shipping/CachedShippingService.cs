using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Models;
using Microsoft.Extensions.Caching.Memory;
using MimeKit.Cryptography;

namespace KKBookstore.Infrastructure.Shipping;

public class CachedShippingService(
    IMemoryCache memoryCache,
    IShippingService wrapee
) : ShippingServiceBaseDecorator(wrapee)
{
    // list of cache keys
    private readonly string _communesCacheKey = "location:communes";
    private readonly string _districtsCacheKey = "location:districts";
    private readonly string _provincesCacheKey = "location:provinces";
    
    private readonly TimeSpan _cacheDuration = TimeSpan.FromHours(1);

    private readonly IMemoryCache _memoryCache = memoryCache;

    public override async Task<Result<GetCommuneResponse>> GetCommuneAsync(int districtId, CancellationToken cancellationToken)
    {
        var key = $"{_communesCacheKey}?districtId:{districtId}";

        if (_memoryCache.TryGetValue(key, out GetCommuneResponse? communes))
        {
            return Result.Success(communes!);
        }
        
        var result = await base.GetCommuneAsync(districtId, cancellationToken);

        if (result.IsSuccess)
        {
            _memoryCache.Set(key, result.Value, _cacheDuration);
        }

        return result;
    }

    public override async Task<Result<GetDistrictResponse>> GetDistrictAsync(int provinceId, CancellationToken cancellationToken)
    {
        var key = $"{_districtsCacheKey}?provinceId:{provinceId}";

        if (_memoryCache.TryGetValue(key, out GetDistrictResponse? districts))
        {
            return Result.Success(districts!);
        }

        var result = await base.GetDistrictAsync(provinceId, cancellationToken);

        if (result.IsSuccess)
        {
            _memoryCache.Set(key, result.Value, _cacheDuration);
        }

        return result;
    }

    public override async Task<Result<GetProvinceResponse>> GetProvinceAsync(CancellationToken cancellationToken)
    {
        if (_memoryCache.TryGetValue(_provincesCacheKey, out GetProvinceResponse? provinces))
        {
            return Result.Success(provinces!);
        }

        var result = await base.GetProvinceAsync(cancellationToken);

        if (result.IsSuccess)
        {
            _memoryCache.Set(_provincesCacheKey, result.Value, _cacheDuration);
        }

        return result;
    }
}
