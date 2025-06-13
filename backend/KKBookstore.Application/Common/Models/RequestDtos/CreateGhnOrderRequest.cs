using KKBookstore.Models;
using KKBookstore.Orders;
using System.Text.Json.Serialization;

namespace KKBookstore.Application.Common.Models.RequestDtos;

/// <summary>
/// Request DTO for creating a GHN shipping order
/// </summary>
public class CreateGhnOrderRequest
{
    // Customer Information
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

    // Sender Information (optional - defaults to shop info)
    [JsonPropertyName("from_name")]
    public string? FromName { get; set; }
    
    [JsonPropertyName("from_phone")]
    public string? FromPhone { get; set; }
    
    [JsonPropertyName("from_address")]
    public string? FromAddress { get; set; }
    
    [JsonPropertyName("from_ward_name")]
    public string? FromWardName { get; set; }
    
    [JsonPropertyName("from_district_name")]
    public string? FromDistrictName { get; set; }
    
    [JsonPropertyName("from_province_name")]
    public string? FromProvinceName { get; set; }

    // Return Information (optional)
    [JsonPropertyName("return_phone")]
    public string? ReturnPhone { get; set; }
    
    [JsonPropertyName("return_address")]
    public string? ReturnAddress { get; set; }
    
    [JsonPropertyName("return_district_id")]
    public int? ReturnDistrictId { get; set; }
    
    [JsonPropertyName("return_ward_code")]
    public string? ReturnWardCode { get; set; }

    // Order Details
    [JsonPropertyName("client_order_code")]
    public string? ClientOrderCode { get; set; } // Our internal order code
    
    [JsonPropertyName("cod_amount")]
    public int CodAmount { get; set; } // Cash on delivery amount
    
    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty; // Order description
    
    [JsonPropertyName("note")]
    public string? Note { get; set; } // Delivery instructions

    // Package Dimensions & Weight
    [JsonPropertyName("weight")]
    public int Weight { get; set; } // in grams
    
    [JsonPropertyName("length")]
    public int Length { get; set; } // in cm
    
    [JsonPropertyName("width")]
    public int Width { get; set; } // in cm
    
    [JsonPropertyName("height")]
    public int Height { get; set; } // in cm

    // Service Configuration
    [JsonPropertyName("service_type_id")]
    public int ServiceTypeId { get; set; } = 2; // 2: E-commerce, 5: Traditional
    
    [JsonPropertyName("payment_type_id")]
    public int PaymentTypeId { get; set; } = 1; // 1: Shop pays, 2: Customer pays
    
    [JsonPropertyName("required_note")]
    public string RequiredNote { get; set; } = "KHONGCHOXEMHANG"; // CHOTHUHANG, CHOXEMHANGKHONGTHU, KHONGCHOXEMHANG
      // Optional Configuration
    [JsonPropertyName("pick_station_id")]
    public int? PickStationId { get; set; }
    
    [JsonPropertyName("insurance_value")]
    public int InsuranceValue { get; set; } = 0;
    
    [JsonPropertyName("coupon")]
    public string? Coupon { get; set; }
    
    [JsonPropertyName("pick_shift")]
    public List<int>? PickShift { get; set; }

    // Items (required for traditional delivery)
    [JsonPropertyName("items")]
    public List<GhnOrderItem> Items { get; set; } = new();
}

/// <summary>
/// Item details for GHN order
/// </summary>
public class GhnOrderItem
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    
    [JsonPropertyName("code")]
    public string? Code { get; set; }
    
    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }
    
    [JsonPropertyName("price")]
    public int? Price { get; set; }
    
    [JsonPropertyName("length")]
    public int? Length { get; set; }
    
    [JsonPropertyName("weight")]
    public int Weight { get; set; }
    
    [JsonPropertyName("width")]
    public int? Width { get; set; }
    
    [JsonPropertyName("height")]
    public int? Height { get; set; }
    
    [JsonPropertyName("category")]
    public GhnItemCategory? Category { get; set; }
}

/// <summary>
/// Category information for GHN items
/// </summary>
public class GhnItemCategory
{
    [JsonPropertyName("level1")]
    public string? Level1 { get; set; }
    
    [JsonPropertyName("level2")]
    public string? Level2 { get; set; }
    
    [JsonPropertyName("level3")]
    public string? Level3 { get; set; }
}
