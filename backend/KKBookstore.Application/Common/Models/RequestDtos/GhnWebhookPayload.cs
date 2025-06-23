using System.Text.Json.Serialization;

namespace KKBookstore.Common.Models.RequestDtos;

/// <summary>
/// Represents the webhook payload structure from GHN delivery service.
/// This DTO is used for deserializing incoming webhook requests.
/// </summary>
public class GhnWebhookPayload
{
    [JsonPropertyName("CODAmount")]
    public decimal? CODAmount { get; set; }

    [JsonPropertyName("CODTransferDate")]
    public string? CODTransferDate { get; set; }

    [JsonPropertyName("ClientOrderCode")]
    public string ClientOrderCode { get; set; } = string.Empty;

    [JsonPropertyName("ConvertedWeight")]
    public int? ConvertedWeight { get; set; }

    [JsonPropertyName("Description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("Fee")]
    public GhnFeeDto? Fee { get; set; }

    [JsonPropertyName("Height")]
    public int? Height { get; set; }

    [JsonPropertyName("IsPartialReturn")]
    public bool? IsPartialReturn { get; set; }

    [JsonPropertyName("Length")]
    public int? Length { get; set; }

    [JsonPropertyName("OrderCode")]
    public string OrderCode { get; set; } = string.Empty;

    [JsonPropertyName("PartialReturnCode")]
    public string PartialReturnCode { get; set; } = string.Empty;

    [JsonPropertyName("PaymentType")]
    public int? PaymentType { get; set; }

    [JsonPropertyName("Reason")]
    public string Reason { get; set; } = string.Empty;

    [JsonPropertyName("ReasonCode")]
    public string ReasonCode { get; set; } = string.Empty;

    [JsonPropertyName("ShopID")]
    public int? ShopID { get; set; }

    [JsonPropertyName("Status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("Time")]
    public string Time { get; set; } = string.Empty;

    [JsonPropertyName("TotalFee")]
    public decimal? TotalFee { get; set; }

    [JsonPropertyName("Type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("Warehouse")]
    public string Warehouse { get; set; } = string.Empty;

    [JsonPropertyName("Weight")]
    public int? Weight { get; set; }

    [JsonPropertyName("Width")]
    public int? Width { get; set; }
}

/// <summary>
/// Represents the fee structure in GHN webhook payload.
/// </summary>
public class GhnFeeDto
{
    [JsonPropertyName("CODFailedFee")]
    public decimal CODFailedFee { get; set; }

    [JsonPropertyName("CODFee")]
    public decimal CODFee { get; set; }

    [JsonPropertyName("Coupon")]
    public decimal Coupon { get; set; }

    [JsonPropertyName("DeliverRemoteAreasFee")]
    public decimal DeliverRemoteAreasFee { get; set; }

    [JsonPropertyName("DocumentReturn")]
    public decimal DocumentReturn { get; set; }

    [JsonPropertyName("DoubleCheck")]
    public decimal DoubleCheck { get; set; }

    [JsonPropertyName("Insurance")]
    public decimal Insurance { get; set; }

    [JsonPropertyName("MainService")]
    public decimal MainService { get; set; }

    [JsonPropertyName("PickRemoteAreasFee")]
    public decimal PickRemoteAreasFee { get; set; }

    [JsonPropertyName("R2S")]
    public decimal R2S { get; set; }

    [JsonPropertyName("Return")]
    public decimal Return { get; set; }

    [JsonPropertyName("StationDO")]
    public decimal StationDO { get; set; }

    [JsonPropertyName("StationPU")]
    public decimal StationPU { get; set; }

    [JsonPropertyName("Total")]
    public decimal Total { get; set; }
}
