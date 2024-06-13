using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Models;

namespace KKBookstore.Infrastructure.Shipping;

public abstract class ShippingServiceBaseDecorator(
    IShippingService wrappee
) : IShippingService
{
    private readonly IShippingService _wrappee = wrappee;

    public virtual async Task<Result<string>> FindCommuneCodeAsync(int districtId, string communeName, CancellationToken cancellationToken)
    {
        return await _wrappee.FindCommuneCodeAsync(districtId, communeName, cancellationToken);
    }

    public virtual async Task<Result<int>> FindDistrictIdAsync(int provinceId, string districtName, CancellationToken cancellationToken)
    {
        return await _wrappee.FindDistrictIdAsync(provinceId, districtName, cancellationToken);
    }

    public virtual async Task<Result<int>> FindProvinceAsync(string provinceName, CancellationToken cancellationToken)
    {
        return await _wrappee.FindProvinceAsync(provinceName, cancellationToken);
    }

    public virtual async Task<Result<GetCommuneResponse>> GetCommuneAsync(int districtId, CancellationToken cancellationToken)
    {
        return await _wrappee.GetCommuneAsync(districtId, cancellationToken);
    }

    public virtual async Task<Result<GetDistrictResponse>> GetDistrictAsync(int provinceId, CancellationToken cancellationToken)
    {
        return await _wrappee.GetDistrictAsync(provinceId, cancellationToken);
    }

    public virtual async Task<Result<DateTimeOffset>> GetExpectedDeliveryTime(ExpectDeliveryTimeRequest request, CancellationToken cancellationToken)
    {
        return await _wrappee.GetExpectedDeliveryTime(request, cancellationToken);
    }

    public virtual async Task<Result<GetProvinceResponse>> GetProvinceAsync(CancellationToken cancellationToken)
    {
        return await _wrappee.GetProvinceAsync(cancellationToken);
    }

    public virtual async Task<Result<ShippingFeeResponse>> GetShippingFeeAsync(ShippingFeeRequest request, CancellationToken cancellationToken)
    {
        return await _wrappee.GetShippingFeeAsync(request, cancellationToken);
    }
}
