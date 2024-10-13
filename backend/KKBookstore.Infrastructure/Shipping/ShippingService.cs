using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Models;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text;

namespace KKBookstore.Infrastructure.Shipping;

public class ShippingService : IShippingService
{
    // httpclient to send request to shipping service
    private readonly HttpClient _httpClient;
    private readonly ShippingConfiguration configuration;

    public ShippingService(IHttpClientFactory httpClientFactory, IOptions<ShippingConfiguration> configurationOption)
    {
        _httpClient = httpClientFactory.CreateClient();
        configuration = configurationOption.Value;

        // Add dedicated header for shipping service
        _httpClient.DefaultRequestHeaders.Add("Token", configuration.Token);
        _httpClient.DefaultRequestHeaders.Add("ShopId", configuration.ShopId.ToString());
    }

    public async Task<Result<GetProvinceResponse>> GetProvinceAsync(CancellationToken cancellationToken)
    {
        try
        {
            var apiEndpoint = configuration.BaseApiUrl + configuration.BaseProvince;
            var result = await _httpClient.GetFromJsonAsync<GetProvinceResponse>(apiEndpoint, cancellationToken);

            return result;
        }
        catch (Exception ex)
        {
            return Result.Failure<GetProvinceResponse>(ShippingServiceErrors.RequestFailed(ex.Message));
        }
    }

    public async Task<Result<GetDistrictResponse>> GetDistrictAsync(int provinceId, CancellationToken cancellationToken)
    {
        try
        {
            var apiEndpoint = configuration.BaseApiUrl + configuration.BaseDistrict + $"?province_id={provinceId}";
            return await _httpClient.GetFromJsonAsync<GetDistrictResponse>(apiEndpoint, cancellationToken);
        }
        catch (Exception ex)
        {
            return Result.Failure<GetDistrictResponse>(ShippingServiceErrors.RequestFailed(ex.Message));
        }
    }

    public async Task<Result<GetCommuneResponse>> GetCommuneAsync(int districtId, CancellationToken cancellationToken)
    {
        try
        {
            var apiEndpoint = configuration.BaseApiUrl + configuration.BaseWard + $"?district_id={districtId}";
            return await _httpClient.GetFromJsonAsync<GetCommuneResponse>(apiEndpoint, cancellationToken);
        }
        catch (Exception ex)
        {
            return Result.Failure<GetCommuneResponse>(ShippingServiceErrors.RequestFailed(ex.Message));
        }
    }

    public async Task<Result<ShippingFeeResponse>> GetShippingFeeAsync(ShippingFeeRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var queryString = $"service_id={request.ServiceId}&service_type_id={request.ServiceTypeId}&to_district_id={request.ToDistrictId}&to_ward_code={request.ToWardCode}&height={request.Height}&length={request.Length}&width={request.Width}&weight={request.Weight}&insurance_value={request.InsuranceValue}&cod_failed_amount={request.CodFailedAmount}";
            var apiEndpoint = $"{configuration.BaseApiUrl}{configuration.Version}/{configuration.BaseShippingFee}?{queryString}";

            var result = await _httpClient.GetFromJsonAsync<ShippingFeeResponse>(apiEndpoint, cancellationToken);
            return result;
        }
        catch (Exception ex)
        {
            return Result.Failure<ShippingFeeResponse>(ShippingServiceErrors.RequestFailed(ex.Message));
        }
    }

    public async Task<Result<int>> FindProvinceAsync(string provinceName, CancellationToken cancellationToken)
    {
        var result = await GetProvinceAsync(cancellationToken);
        if (result.IsFailure)
        {
            return Result.Failure<int>(result.Error);
        }
        var provinceId = result.Value.Provinces.Find(p => p.Name.Contains(provinceName))?.Id ?? 0;

        if (provinceId == 0)
        {
            return Result.Failure<int>(ShippingServiceErrors.InvalidProvinceName);
        }

        return provinceId;
    }

    public async Task<Result<int>> FindDistrictIdAsync(int provinceId, string districtName, CancellationToken cancellationToken)
    {
        var result = await GetDistrictAsync(provinceId, cancellationToken);

        if (result.IsFailure)
        {
            return Result.Failure<int>(result.Error);
        }

        var districtId = result.Value.Districts.Find(d => d.Name.Contains(districtName))?.Id ?? 0;

        if (districtId == 0)
        {
            return Result.Failure<int>(ShippingServiceErrors.InvalidDistrictName);
        }

        return districtId;
    }

    public async Task<Result<string>> FindCommuneCodeAsync(int districtId, string communeName, CancellationToken cancellationToken)
    {
        var result = await GetCommuneAsync(districtId, cancellationToken);

        if (result.IsFailure)
        {
            return Result.Failure<string>(result.Error);
        }

        var communeCode = result.Value.Communes.Find(c => c.Name.Contains(communeName))?.Code ?? string.Empty;

        if (string.IsNullOrEmpty(communeCode))
        {
            return Result.Failure<string>(ShippingServiceErrors.InvalidCommuneName);
        }

        return communeCode;
    }

    public async Task<Result<DateTimeOffset>> GetExpectedDeliveryTime(ExpectDeliveryTimeRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var apiEndpoint = $"{configuration.BaseApiUrl}{configuration.Version}/{configuration.BaseExpectedDeliveryTime}";
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(apiEndpoint, content, cancellationToken);

            var responseBody = await response.Content.ReadFromJsonAsync<ExpectDeliveryTimeResponse>(cancellationToken: cancellationToken);

            if (responseBody == null)
            {
                var errors = await response.Content.ReadAsStringAsync(cancellationToken);

                return Result.Failure<DateTimeOffset>(ShippingServiceErrors.RequestFailed(errors));
            }

            var result = DateTimeOffset.FromUnixTimeSeconds(responseBody.Data.LeadTimeUnix);

            return result;
        }
        catch (Exception ex)
        {
            return Result.Failure<DateTimeOffset>(ShippingServiceErrors.RequestFailed(ex.Message));
        }
    }
}
