using KKBookstore.Common.Interfaces;
using KKBookstore.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text;

namespace KKBookstore.Shipping;

public class ShippingService : IShippingService
{
    // httpclient to send request to shipping service
    private readonly HttpClient _httpClient;
    private readonly ShippingConfiguration configuration;
    private readonly ILogger<ShippingService> _logger;

    public ShippingService(IHttpClientFactory httpClientFactory, IOptions<ShippingConfiguration> configurationOption, ILogger<ShippingService> logger)
    {
        _httpClient = httpClientFactory.CreateClient();
        configuration = configurationOption.Value;

        // Add dedicated header for shipping service
        _httpClient.DefaultRequestHeaders.Add("Token", configuration.Token);
        _httpClient.DefaultRequestHeaders.Add("ShopId", configuration.ShopId.ToString());
        _logger = logger;
    }

    public async Task<Result<GetProvinceResponse>> GetProvinceAsync(CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Fetching provinces from shipping service.");
            var apiEndpoint = configuration.BaseApiUrl + configuration.BaseProvince;
            var result = await _httpClient.GetFromJsonAsync<GetProvinceResponse>(apiEndpoint, cancellationToken);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching provinces.");
            return Result.Failure<GetProvinceResponse>(ShippingServiceErrors.RequestFailed(ex.Message));
        }
    }

    public async Task<Result<GetDistrictResponse>> GetDistrictAsync(int provinceId, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Fetching districts for province ID {ProvinceId} from shipping service.", provinceId);
            var apiEndpoint = configuration.BaseApiUrl + configuration.BaseDistrict + $"?province_id={provinceId}";
            return await _httpClient.GetFromJsonAsync<GetDistrictResponse>(apiEndpoint, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching districts for province ID {ProvinceId}.", provinceId);
            return Result.Failure<GetDistrictResponse>(ShippingServiceErrors.RequestFailed(ex.Message));
        }
    }

    public async Task<Result<GetCommuneResponse>> GetCommuneAsync(int districtId, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Fetching communes for district ID {DistrictId} from shipping service.", districtId);
            var apiEndpoint = configuration.BaseApiUrl + configuration.BaseWard + $"?district_id={districtId}";
            return await _httpClient.GetFromJsonAsync<GetCommuneResponse>(apiEndpoint, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching communes for district ID {DistrictId}.", districtId);
            return Result.Failure<GetCommuneResponse>(ShippingServiceErrors.RequestFailed(ex.Message));
        }
    }
    public async Task<Result<ShippingFeeResponse>> GetShippingFeeAsync(ShippingFeeRequest request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Calculating shipping fee with request: {@Request}", request);

            // Fetch available services to get the service with the lowest service_id
            var availableServicesResult = await GetAvailableServicesAsync(request.ToDistrictId, cancellationToken);
            if (availableServicesResult.IsFailure)
            {
                _logger.LogError("Failed to fetch available services for district ID {ToDistrictId}: {Error}",
                    request.ToDistrictId, availableServicesResult.Error);
                return Result.Failure<ShippingFeeResponse>(availableServicesResult.Error);
            }

            var availableServices = availableServicesResult.Value.Data;
            if (availableServices == null || !availableServices.Any())
            {
                _logger.LogWarning("No available services found for district ID {ToDistrictId}", request.ToDistrictId);
                return Result.Failure<ShippingFeeResponse>(ShippingServiceErrors.RequestFailed("No available services found for the destination district"));
            }

            // Select the service with the lowest service_id
            var selectedService = availableServices.OrderBy(s => s.ServiceId).First();
            _logger.LogInformation("Selected service with ID {ServiceId} and type ID {ServiceTypeId} for district {ToDistrictId}",
                selectedService.ServiceId, selectedService.ServiceTypeId, request.ToDistrictId);

            // Update the request with the dynamic service information
            request.ServiceId = selectedService.ServiceId;
            request.ServiceTypeId = selectedService.ServiceTypeId;

            if (request.Weight <= 0)
            {
                request.Weight = 200;
            }

            var queryString = $"service_id={request.ServiceId}&service_type_id={request.ServiceTypeId}&to_district_id={request.ToDistrictId}&to_ward_code={request.ToWardCode}&height={request.Height}&length={request.Length}&width={request.Width}&weight={request.Weight}&insurance_value={request.InsuranceValue}&cod_failed_amount={request.CodFailedAmount}";
            var apiEndpoint = $"{configuration.BaseApiUrl}{configuration.Version}/{configuration.BaseShippingFee}?{queryString}";

            var result = await _httpClient.GetFromJsonAsync<ShippingFeeResponse>(apiEndpoint, cancellationToken);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calculating shipping fee with request: {@Request}", request);
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
            _logger.LogInformation("Calculating expected delivery time with request: {@Request}", request);
            var apiEndpoint = $"{configuration.BaseApiUrl}{configuration.Version}/{configuration.BaseExpectedDeliveryTime}";
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(apiEndpoint, content, cancellationToken);

            var responseBody = await response.Content.ReadFromJsonAsync<ExpectDeliveryTimeResponse>(cancellationToken: cancellationToken);

            if (responseBody == null)
            {
                var errors = await response.Content.ReadAsStringAsync(cancellationToken);

                return Result.Failure<DateTimeOffset>(ShippingServiceErrors.RequestFailed(errors));
            }

            _logger.LogError("Failed to calculate expected delivery time. Status code: {StatusCode}, Response: {Response}", response.StatusCode, responseBody);
            var result = DateTimeOffset.FromUnixTimeSeconds(responseBody.Data.LeadTimeUnix);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calculating expected delivery time with request: {@Request}", request);
            return Result.Failure<DateTimeOffset>(ShippingServiceErrors.RequestFailed(ex.Message));
        }
    }

    public async Task<Result<AvailableServicesResponse>> GetAvailableServicesAsync(int toDistrictId, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Fetching available services for district ID {ToDistrictId} from shipping service.", toDistrictId);

            var request = new AvailableServicesRequest
            {
                ShopId = configuration.ShopId,
                FromDistrict = configuration.FromDistrictId,
                ToDistrict = toDistrictId
            };

            var apiEndpoint = $"{configuration.BaseApiUrl}{configuration.Version}/{configuration.BaseAvailableServices}";
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(apiEndpoint, content, cancellationToken);
            var result = await response.Content.ReadFromJsonAsync<AvailableServicesResponse>(cancellationToken: cancellationToken);

            if (result == null)
            {
                var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                return Result.Failure<AvailableServicesResponse>(ShippingServiceErrors.RequestFailed(errorContent));
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching available services for district ID {ToDistrictId}.", toDistrictId);
            return Result.Failure<AvailableServicesResponse>(ShippingServiceErrors.RequestFailed(ex.Message));
        }
    }
}
