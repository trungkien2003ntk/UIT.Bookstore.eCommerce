using KKBookstore.Domain.Models;
using System.Text.Json.Serialization;

namespace KKBookstore.Application.Common.Interfaces;

public partial interface IShippingService
{
    Task<Result<ShippingFeeResponse>> GetShippingFeeAsync(ShippingFeeRequest request, CancellationToken cancellationToken);
    Task<Result<int>> FindProvinceAsync(string provinceName, CancellationToken cancellationToken);
    Task<Result<int>> FindDistrictIdAsync(int provinceId, string districtName, CancellationToken cancellationToken);
    Task<Result<string>> FindCommuneCodeAsync(int districtId, string communeName, CancellationToken cancellationToken);
    Task<Result<DateTimeOffset>> GetExpectedDeliveryTime(ExpectDeliveryTimeRequest request, CancellationToken cancellationToken);  
}

public record ShippingFeeRequest
{
    public int ServiceId { get; private init; } = 53320;
    public int ServiceTypeId { get; private init; } = 2;
    public int ToDistrictId { get; set; }
    public string ToWardCode { get; set; }
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