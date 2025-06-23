using System.Text.Json.Serialization;

namespace KKBookstore.Common.Models.RequestDtos;

/// <summary>
/// Request DTO for calculating GHN delivery time
/// </summary>
public class CalculateGhnDeliveryTimeRequest
{
    [JsonPropertyName("from_district_id")]
    public int FromDistrictId { get; set; }

    [JsonPropertyName("from_ward_code")]
    public string FromWardCode { get; set; } = string.Empty;

    [JsonPropertyName("to_district_id")]
    public int ToDistrictId { get; set; }

    [JsonPropertyName("to_ward_code")]
    public string ToWardCode { get; set; } = string.Empty;

    [JsonPropertyName("service_id")]
    public int ServiceId { get; set; }
}

/// <summary>
/// Request DTO for creating a GHN store/branch
/// </summary>
public class CreateGhnStoreRequest
{
    [JsonPropertyName("district_id")]
    public int DistrictId { get; set; }

    [JsonPropertyName("ward_code")]
    public string WardCode { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("phone")]
    public string Phone { get; set; } = string.Empty;

    [JsonPropertyName("address")]
    public string Address { get; set; } = string.Empty;
}

/// <summary>
/// Request DTO for updating COD amount
/// </summary>
public class UpdateGhnCodRequest
{
    [JsonPropertyName("order_code")]
    public string OrderCode { get; set; } = string.Empty;

    [JsonPropertyName("cod_amount")]
    public int CodAmount { get; set; }
}
