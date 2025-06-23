using KKBookstore.Models;
using System.Text.Json.Serialization;

namespace KKBookstore.Common.Interfaces;

public partial interface IShippingService
{
    Task<Result<ShippingFeeResponse>> GetShippingFeeAsync(ShippingFeeRequest request, CancellationToken cancellationToken);
    Task<Result<int>> FindProvinceAsync(string provinceName, CancellationToken cancellationToken);
    Task<Result<int>> FindDistrictIdAsync(int provinceId, string districtName, CancellationToken cancellationToken);
    Task<Result<string>> FindCommuneCodeAsync(int districtId, string communeName, CancellationToken cancellationToken);
    Task<Result<DateTimeOffset>> GetExpectedDeliveryTime(ExpectDeliveryTimeRequest request, CancellationToken cancellationToken);
    Task<Result<AvailableServicesResponse>> GetAvailableServicesAsync(int toDistrictId, CancellationToken cancellationToken);
}

public record ShippingFeeRequest
{
    public int ServiceId { get; set; } = 53322;
    public int ServiceTypeId { get; set; } = 2;
    public int ToDistrictId { get; set; }
    public string ToWardCode { get; set; } = string.Empty;
    public int Height { get; set; }
    public int Length { get; set; }
    public int Width { get; set; }
    public int Weight { get; set; }
    public int InsuranceValue { get; set; }
    public int CodFailedAmount { get; private init; } = 0;
}

public record ShippingFeeResponse
{
    [JsonPropertyName("data")]
    public DataDto Data { get; set; }

    public class DataDto
    {
        [JsonPropertyName("total")]
        public int Total { get; set; }
        [JsonPropertyName("service_fee")]
        public int ServiceFee { get; set; }
        [JsonPropertyName("insurance_fee")]
        public int InsuranceFee { get; set; }
    }
}

public record ExpectDeliveryTimeRequest
{
    [JsonPropertyName("service_id")]
    public int ServiceId { get; private init; } = 53320;
    [JsonPropertyName("from_district_id")]
    public int FromDistrictId { get; set; }
    [JsonPropertyName("to_district_id")]
    public int ToDistrictId { get; set; }
    [JsonPropertyName("from_ward_code")]
    public string FromWardCode { get; set; }
    [JsonPropertyName("to_ward_code")]
    public string ToWardCode { get; set; }
}

public record ExpectDeliveryTimeResponse
{
    [JsonPropertyName("data")]
    public DataDto Data { get; set; }

    public sealed record DataDto
    {
        [JsonPropertyName("leadtime")]
        public int LeadTimeUnix { get; set; }
    }
}

public record AvailableServicesRequest
{
    [JsonPropertyName("shop_id")]
    public int ShopId { get; set; }

    [JsonPropertyName("from_district")]
    public int FromDistrict { get; set; }

    [JsonPropertyName("to_district")]
    public int ToDistrict { get; set; }
}

public record AvailableServicesResponse
{
    [JsonPropertyName("code")]
    public int Code { get; set; }

    [JsonPropertyName("code_message_value")]
    public string CodeMessageValue { get; set; } = string.Empty;

    [JsonPropertyName("data")]
    public List<ServiceDto> Data { get; set; } = new();

    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
}

public record ServiceDto
{
    [JsonPropertyName("service_id")]
    public int ServiceId { get; set; }

    [JsonPropertyName("short_name")]
    public string ShortName { get; set; } = string.Empty;

    [JsonPropertyName("service_type_id")]
    public int ServiceTypeId { get; set; }

    [JsonPropertyName("config_fee_id")]
    public string ConfigFeeId { get; set; } = string.Empty;

    [JsonPropertyName("extra_cost_id")]
    public string ExtraCostId { get; set; } = string.Empty;

    [JsonPropertyName("standard_config_fee_id")]
    public string StandardConfigFeeId { get; set; } = string.Empty;

    [JsonPropertyName("standard_extra_cost_id")]
    public string StandardExtraCostId { get; set; } = string.Empty;

    [JsonPropertyName("ecom_config_fee_id")]
    public int EcomConfigFeeId { get; set; }

    [JsonPropertyName("ecom_extra_cost_id")]
    public int EcomExtraCostId { get; set; }

    [JsonPropertyName("ecom_standard_config_fee_id")]
    public int EcomStandardConfigFeeId { get; set; }

    [JsonPropertyName("ecom_standard_extra_cost_id")]
    public int EcomStandardExtraCostId { get; set; }
}