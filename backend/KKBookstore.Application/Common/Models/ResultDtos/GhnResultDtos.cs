using KKBookstore.Models;
using KKBookstore.Orders;
using System.Text.Json.Serialization;

namespace KKBookstore.Application.Common.Models.ResultDtos;

/// <summary>
/// Fee breakdown for GHN order
/// </summary>
public class GhnOrderFee
{
    [JsonPropertyName("coupon")]
    public int Coupon { get; set; }
    
    [JsonPropertyName("insurance")]
    public int Insurance { get; set; }
    
    [JsonPropertyName("main_service")]
    public int MainService { get; set; }
    
    [JsonPropertyName("r2s")]
    public int R2s { get; set; } // Fee of delivery parcel again
    
    [JsonPropertyName("return")]
    public int Return { get; set; } // Fee of Return to ship
    
    [JsonPropertyName("station_do")]
    public int StationDo { get; set; } // Pickup fee at Station
    
    [JsonPropertyName("station_pu")]
    public int StationPu { get; set; } // Delivery fee at Station
}

/// <summary>
/// Response DTO for GHN order creation
/// </summary>
public class CreateGhnOrderResult
{
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
    
    // Success Response Data
    [JsonPropertyName("order_code")]
    public string? OrderCode { get; set; } // GHN tracking code
    
    [JsonPropertyName("sort_code")]
    public string? SortCode { get; set; }
    
    [JsonPropertyName("trans_type")]
    public string? TransType { get; set; } // Transportation type
    
    [JsonPropertyName("district_encode")]
    public string? DistrictEncode { get; set; }
    
    [JsonPropertyName("ward_encode")]
    public string? WardEncode { get; set; }
    
    [JsonPropertyName("expected_delivery_time")]
    public DateTime? ExpectedDeliveryTime { get; set; }
    
    [JsonPropertyName("fee")]
    public GhnOrderFee? Fee { get; set; }
    
    [JsonPropertyName("total_fee")]
    public int TotalFee { get; set; }
}

/// <summary>
/// Response DTO for GHN order information
/// </summary>
public class GhnOrderInfoResult
{
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
    
    // Order Basic Info
    [JsonPropertyName("shop_id")]
    public int ShopId { get; set; }
    
    [JsonPropertyName("client_id")]
    public int ClientId { get; set; }
    
    [JsonPropertyName("order_code")]
    public string OrderCode { get; set; } = string.Empty;
    
    [JsonPropertyName("client_order_code")]
    public string? ClientOrderCode { get; set; }
    
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;
    
    // Customer Info
    [JsonPropertyName("to_name")]
    public string ToName { get; set; } = string.Empty;
    
    [JsonPropertyName("to_phone")]
    public string ToPhone { get; set; } = string.Empty;
    
    [JsonPropertyName("to_address")]
    public string ToAddress { get; set; } = string.Empty;
    
    [JsonPropertyName("to_ward_code")]
    public string ToWardCode { get; set; } = string.Empty;
    
    [JsonPropertyName("to_district_id")]
    public int ToDistrictId { get; set; }
    
    // Sender Info
    [JsonPropertyName("from_name")]
    public string FromName { get; set; } = string.Empty;
    
    [JsonPropertyName("from_phone")]
    public string FromPhone { get; set; } = string.Empty;
    
    [JsonPropertyName("from_address")]
    public string FromAddress { get; set; } = string.Empty;
    
    [JsonPropertyName("from_ward_code")]
    public string FromWardCode { get; set; } = string.Empty;
    
    [JsonPropertyName("from_district_id")]
    public int FromDistrictId { get; set; }
    
    // Return Info
    [JsonPropertyName("return_name")]
    public string? ReturnName { get; set; }
    
    [JsonPropertyName("return_phone")]
    public string? ReturnPhone { get; set; }
    
    [JsonPropertyName("return_address")]
    public string? ReturnAddress { get; set; }
      [JsonPropertyName("return_ward_code")]
    public string? ReturnWardCode { get; set; }
    
    [JsonPropertyName("return_district_id")]
    public int? ReturnDistrictId { get; set; }
    
    // Package Details
    [JsonPropertyName("weight")]
    public int Weight { get; set; }
    
    [JsonPropertyName("length")]
    public int Length { get; set; }
    
    [JsonPropertyName("width")]
    public int Width { get; set; }
    
    [JsonPropertyName("height")]
    public int Height { get; set; }
    
    [JsonPropertyName("converted_weight")]
    public int ConvertedWeight { get; set; }
    
    // Service Details
    [JsonPropertyName("service_type_id")]
    public int ServiceTypeId { get; set; }
    
    [JsonPropertyName("service_id")]
    public int ServiceId { get; set; }
    
    [JsonPropertyName("payment_type_id")]
    public int PaymentTypeId { get; set; }
    
    // Financial Info
    [JsonPropertyName("cod_amount")]
    public int CodAmount { get; set; }
    public DateTime? CodCollectDate { get; set; }    [JsonPropertyName("cod_transfer_date")]
    public DateTime? CodTransferDate { get; set; }
    
    [JsonPropertyName("is_cod_transferred")]
    public bool IsCodTransferred { get; set; }
    
    [JsonPropertyName("is_cod_collected")]
    public bool IsCodCollected { get; set; }
    
    [JsonPropertyName("cod_failed_amount")]
    public int CodFailedAmount { get; set; }
    
    [JsonPropertyName("cod_failed_collect_date")]
    public DateTime? CodFailedCollectDate { get; set; }
    
    [JsonPropertyName("insurance_value")]
    public int InsuranceValue { get; set; }
    
    [JsonPropertyName("order_value")]
    public int OrderValue { get; set; }
    
    [JsonPropertyName("custom_service_fee")]
    public int CustomServiceFee { get; set; }
    
    // Delivery Details
    [JsonPropertyName("required_note")]
    public string RequiredNote { get; set; } = string.Empty;
    
    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;
    
    [JsonPropertyName("note")]
    public string? Note { get; set; }
    
    [JsonPropertyName("employee_note")]
    public string? EmployeeNote { get; set; }
    
    [JsonPropertyName("coupon")]
    public string? Coupon { get; set; }
    
    [JsonPropertyName("pick_station_id")]
    public int PickStationId { get; set; }
    
    [JsonPropertyName("deliver_station_id")]
    public int DeliverStationId { get; set; }
    
    // Warehouse Info
    [JsonPropertyName("pick_warehouse_id")]
    public int PickWarehouseId { get; set; }
    
    [JsonPropertyName("deliver_warehouse_id")]
    public int DeliverWarehouseId { get; set; }
    
    [JsonPropertyName("current_warehouse_id")]
    public int CurrentWarehouseId { get; set; }
    
    [JsonPropertyName("return_warehouse_id")]
    public int ReturnWarehouseId { get; set; }
    
    [JsonPropertyName("next_warehouse_id")]
    public int NextWarehouseId { get; set; }
    
    // Timestamps
    [JsonPropertyName("leadtime")]
    public DateTime? LeadTime { get; set; } // Expected delivery time
    
    [JsonPropertyName("order_date")]
    public DateTime OrderDate { get; set; }
    
    [JsonPropertyName("finish_date")]
    public DateTime? FinishDate { get; set; }
    
    [JsonPropertyName("created_date")]
    public DateTime CreatedDate { get; set; }
    
    [JsonPropertyName("updated_date")]
    public DateTime UpdatedDate { get; set; }
    
    // Status History
    public List<GhnStatusLog> Log { get; set; } = new();
    public List<string> Tag { get; set; } = new();
}

/// <summary>
/// Status change log entry
/// </summary>
public class GhnStatusLog
{
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;
    
    [JsonPropertyName("updated_date")]
    public DateTime UpdatedDate { get; set; }
}

/// <summary>
/// Response DTO for GHN delivery time calculation
/// </summary>
public class GhnDeliveryTimeResult
{
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
    
    [JsonPropertyName("leadtime")]
    public long LeadTime { get; set; } // Unix timestamp
    
    [JsonPropertyName("order_date")]
    public long OrderDate { get; set; } // Unix timestamp
    
    public DateTime? LeadTimeDateTime => DateTimeOffset.FromUnixTimeSeconds(LeadTime).DateTime;
    public DateTime? OrderDateTime => DateTimeOffset.FromUnixTimeSeconds(OrderDate).DateTime;
}

/// <summary>
/// Response DTO for GHN pick shift information
/// </summary>
public class GhnPickShiftResult
{
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
    
    public List<GhnPickShift> Shifts { get; set; } = new();
}

/// <summary>
/// Pick shift information
/// </summary>
public class GhnPickShift
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;
    
    [JsonPropertyName("from_time")]
    public int FromTime { get; set; } // Seconds from midnight
    
    [JsonPropertyName("to_time")]
    public int ToTime { get; set; } // Seconds from midnight
    
    public TimeSpan FromTimeSpan => TimeSpan.FromSeconds(FromTime);
    public TimeSpan ToTimeSpan => TimeSpan.FromSeconds(ToTime);
}

/// <summary>
/// Response DTO for GHN store creation
/// </summary>
public class CreateGhnStoreResult
{
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
    
    [JsonPropertyName("shop_id")]
    public int? ShopId { get; set; }
}

/// <summary>
/// Response DTO for GHN order operations (cancel, return, update COD)
/// </summary>
public class GhnOrderOperationResult
{
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
    
    public List<GhnOrderOperationItem> Results { get; set; } = new();
}

/// <summary>
/// Individual order operation result
/// </summary>
public class GhnOrderOperationItem
{
    [JsonPropertyName("order_code")]
    public string OrderCode { get; set; } = string.Empty;
    
    [JsonPropertyName("result")]
    public bool Result { get; set; }
    
    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
}
