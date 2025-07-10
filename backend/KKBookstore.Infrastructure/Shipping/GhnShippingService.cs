using KKBookstore.Common.Interfaces;
using KKBookstore.Common.Models.RequestDtos;
using KKBookstore.Common.Models.ResultDtos;
using KKBookstore.Models;
using KKBookstore.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace KKBookstore.Shipping;

/// <summary>
/// Comprehensive implementation of GHN shipping service for order management and tracking.
/// Provides full integration with GHN's shipping APIs including order creation, tracking, and management.
/// </summary>
public class GhnShippingService : IGhnShippingService
{
    private readonly HttpClient _httpClient;
    private readonly IApplicationDbContext _context;
    private readonly ILogger<GhnShippingService> _logger;
    private readonly GhnConfiguration _config;

    public GhnShippingService(
        HttpClient httpClient,
        IApplicationDbContext context,
        ILogger<GhnShippingService> logger,
        IOptions<GhnConfiguration> config)
    {
        _httpClient = httpClient;
        _context = context;
        _logger = logger;
        _config = config.Value;

        // Configure HttpClient with base settings
        _httpClient.BaseAddress = new Uri(_config.EffectiveBaseUrl);
        _httpClient.Timeout = TimeSpan.FromSeconds(_config.TimeoutSeconds);
        _httpClient.DefaultRequestHeaders.Add("Token", _config.Token);
        _httpClient.DefaultRequestHeaders.Add("ShopId", _config.ShopId.ToString());
    }

    #region Status Mapping & Webhook Processing    
    public OrderStatus MapGhnStatusToOrderStatus(string ghnStatus)
    {
        _logger.LogDebug("Mapping GHN status '{GhnStatus}' to internal OrderStatus", ghnStatus);

        return ghnStatus.ToLowerInvariant().Replace(" ", "_").Replace("/", "_") switch
        {
            "ready_to_pick" => OrderStatus.Processing,
            "picking" => OrderStatus.Processing,
            "cancel" => OrderStatus.Cancelled,
            "money_collect_picking" => OrderStatus.Processing,
            "picked" => OrderStatus.Processing,
            "storing" => OrderStatus.Processing,
            "transporting" => OrderStatus.Shipped,
            "sorting" => OrderStatus.Shipped,
            "delivering" => OrderStatus.Shipped,
            "money_collect_delivering" => OrderStatus.Shipped,
            "delivered" => OrderStatus.Delivered,
            "delivery_fail" => OrderStatus.Processing, // Keep as processing for retry
            "waiting_to_return" => OrderStatus.Processing,
            "return" => OrderStatus.Processing,
            "return_transporting" => OrderStatus.Processing,
            "return_sorting" => OrderStatus.Processing,
            "returning" => OrderStatus.Processing,
            "return_fail" => OrderStatus.Processing,
            "returned" => OrderStatus.Refunded,
            "exception" => OrderStatus.Processing, // Handle exceptions case by case
            "damage" => OrderStatus.Cancelled,
            "lost" => OrderStatus.Cancelled,
            _ => OrderStatus.Processing // Default fallback
        };
    }

    public GhnOrderStatus MapStringToGhnOrderStatus(string ghnStatus)
    {
        _logger.LogDebug("Mapping GHN status string '{GhnStatus}' to GhnOrderStatus enum", ghnStatus);

        return ghnStatus.ToLower() switch
        {
            "ready_to_pick" => GhnOrderStatus.ReadyToPick,
            "picking" => GhnOrderStatus.Picking,
            "cancel" => GhnOrderStatus.Cancel,
            "money_collect_picking" => GhnOrderStatus.MoneyCollectPicking,
            "picked" => GhnOrderStatus.Picked,
            "storing" => GhnOrderStatus.Storing,
            "transporting" => GhnOrderStatus.Transporting,
            "sorting" => GhnOrderStatus.Sorting,
            "delivering" => GhnOrderStatus.Delivering,
            "money_collect_delivering" => GhnOrderStatus.MoneyCollectDelivering,
            "delivered" => GhnOrderStatus.Delivered,
            "delivery_fail" => GhnOrderStatus.DeliveryFail,
            "waiting_to_return" => GhnOrderStatus.WaitingToReturn,
            "return" => GhnOrderStatus.Return,
            "return_transporting" => GhnOrderStatus.ReturnTransporting,
            "return_sorting" => GhnOrderStatus.ReturnSorting,
            "returning" => GhnOrderStatus.Returning,
            "return_fail" => GhnOrderStatus.ReturnFail,
            "returned" => GhnOrderStatus.Returned,
            "exception" => GhnOrderStatus.Exception,
            "damage" => GhnOrderStatus.Damage,
            "lost" => GhnOrderStatus.Lost,
            _ => GhnOrderStatus.ReadyToPick // Default fallback
        };
    }
    public async Task<Result> ProcessOrderStatusUpdateAsync(string orderCode, string ghnStatus, string? reason = null)
    {
        return await ProcessOrderStatusUpdateAsync(orderCode, ghnStatus, reason, null);
    }

    public async Task<Result> ProcessOrderStatusUpdateAsync(string orderCode, string ghnStatus, string? reason = null, int? triggeredByUserId = null)
    {
        try
        {
            _logger.LogInformation("Processing order status update for GHN order {OrderCode} to status {Status}. Triggered by user: {UserId}",
                orderCode, ghnStatus, triggeredByUserId);

            // Find order by GHN order code
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.OrderNumber == orderCode);

            if (order == null)
            {
                _logger.LogWarning("Order not found for GHN order code {OrderCode}", orderCode);
                return Result.Failure(GhnShippingErrors.OrderNotFound);
            }

            // Map status and update order
            var newStatus = MapGhnStatusToOrderStatus(ghnStatus);
            var previousStatus = order.Status;

            // Skip if status hasn't changed
            if (previousStatus == newStatus)
            {
                _logger.LogInformation("Order {OrderId} status unchanged ({Status}), skipping update", order.Id, newStatus);
                return Result.Success();
            }

            order.Status = newStatus;
            order.LastModificationTime = DateTime.UtcNow;

            // Add status change reason if provided
            var statusChangeNote = string.Empty;
            if (!string.IsNullOrEmpty(reason))
            {
                statusChangeNote = $"GHN Status Change: {reason}";
                order.Comment = string.IsNullOrEmpty(order.Comment)
                    ? statusChangeNote
                    : $"{order.Comment}\n{statusChangeNote}";
            }

            // Record order history
            var actionDescription = triggeredByUserId.HasValue
                ? $"Cập nhật trạng thái GHN thủ công bởi quản trị viên, trạng thái mới: {newStatus}"
                : "Cập nhật trạng thái GHN tự động từ webhook";

            var orderHistory = OrderHistory.Create(
                orderId: order.Id,
                fromStatus: previousStatus,
                toStatus: newStatus,
                action: actionDescription,
                notes: statusChangeNote,
                triggeredByUserId: triggeredByUserId,
                externalReference: orderCode
            );

            if (orderHistory.IsSuccess)
            {
                await _context.OrderHistories.AddAsync(orderHistory.Value);
            }

            await _context.SaveChangesAsync();

            _logger.LogInformation("Successfully updated order {OrderId} from {PreviousStatus} to {NewStatus}",
                order.Id, previousStatus, newStatus);

            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing GHN webhook for order {OrderCode}", orderCode);
            return Result.Failure(GhnShippingErrors.WebhookProcessingFailed);
        }
    }

    #endregion

    #region Order Management

    public async Task<CreateGhnOrderResult> CreateOrderAsync(CreateGhnOrderRequest request)
    {
        try
        {
            _logger.LogInformation("Creating GHN order for client order {ClientOrderCode}", request.ClientOrderCode);

            var payload = new
            {
                payment_type_id = request.PaymentTypeId,
                note = request.Note ?? "",
                required_note = request.RequiredNote,
                from_name = request.FromName,
                from_phone = request.FromPhone,
                from_address = request.FromAddress,
                from_ward_name = request.FromWardName,
                from_district_name = request.FromDistrictName,
                from_province_name = request.FromProvinceName,
                return_phone = request.ReturnPhone,
                return_address = request.ReturnAddress,
                return_district_id = request.ReturnDistrictId,
                return_ward_code = request.ReturnWardCode,
                client_order_code = request.ClientOrderCode,
                to_name = request.ToName,
                to_phone = request.ToPhone,
                to_address = request.ToAddress,
                to_ward_code = request.ToWardCode,
                to_district_id = request.ToDistrictId,
                cod_amount = request.CodAmount,
                content = request.Content,
                weight = request.Weight,
                length = request.Length,
                width = request.Width,
                height = request.Height,
                pick_station_id = request.PickStationId,
                insurance_value = request.InsuranceValue,
                service_type_id = request.ServiceTypeId,
                coupon = request.Coupon,
                pick_shift = request.PickShift,
                items = request.Items.Select(item => new
                {
                    name = item.Name,
                    code = item.Code,
                    quantity = item.Quantity,
                    price = item.Price,
                    length = item.Length,
                    width = item.Width,
                    height = item.Height,
                    weight = item.Weight,
                    category = item.Category != null ? new
                    {
                        level1 = item.Category.Level1,
                        level2 = item.Category.Level2,
                        level3 = item.Category.Level3
                    } : null
                }).ToArray()
            };

            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/shipping-order/create", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("GHN order creation failed: {StatusCode} - {Content}", response.StatusCode, responseContent);
                return new CreateGhnOrderResult
                {
                    Success = false,
                    ErrorMessage = $"GHN API error: {response.StatusCode}"
                };
            }

            var ghnResponse = JsonSerializer.Deserialize<GhnApiResponse<GhnCreateOrderData>>(responseContent);

            if (ghnResponse?.Code != 200)
            {
                _logger.LogError("GHN order creation failed: {Message}", ghnResponse?.Message);
                return new CreateGhnOrderResult
                {
                    Success = false,
                    ErrorMessage = ghnResponse?.Message ?? "Unknown error"
                };
            }

            var data = ghnResponse.Data;
            _logger.LogInformation("Successfully created GHN order {OrderCode} for client order {ClientOrderCode}",
                data.OrderCode, request.ClientOrderCode);

            return new CreateGhnOrderResult
            {
                Success = true,
                OrderCode = data.OrderCode,
                SortCode = data.SortCode,
                TransType = data.TransType,
                DistrictEncode = data.DistrictEncode,
                WardEncode = data.WardEncode,
                ExpectedDeliveryTime = DateTime.TryParse(data.ExpectedDeliveryTime, out var deliveryTime) ? deliveryTime : null,
                Fee = new GhnOrderFee
                {
                    MainService = data.Fee.MainService,
                    Insurance = data.Fee.Insurance,
                    StationDo = data.Fee.StationDo,
                    StationPu = data.Fee.StationPu,
                    Return = data.Fee.Return,
                    R2s = data.Fee.R2S,
                    Coupon = data.Fee.Coupon
                },
                TotalFee = data.TotalFee
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating GHN order for client order {ClientOrderCode}", request.ClientOrderCode);
            return new CreateGhnOrderResult
            {
                Success = false,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<GhnOrderInfoResult> GetOrderInfoAsync(string orderCode)
    {
        try
        {
            _logger.LogInformation("Getting GHN order info for {OrderCode}", orderCode);

            var payload = new { order_code = orderCode };
            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/shipping-order/detail", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("GHN order info failed: {StatusCode} - {Content}", response.StatusCode, responseContent);
                return new GhnOrderInfoResult
                {
                    Success = false,
                    ErrorMessage = $"GHN API error: {response.StatusCode}"
                };
            }

            var ghnResponse = JsonSerializer.Deserialize<GhnApiResponse<GhnOrderInfoData>>(responseContent);

            if (ghnResponse?.Code != 200)
            {
                _logger.LogError("GHN order info failed: {Message}", ghnResponse?.Message);
                return new GhnOrderInfoResult
                {
                    Success = false,
                    ErrorMessage = ghnResponse?.Message ?? "Unknown error"
                };
            }

            var data = ghnResponse.Data;
            return new GhnOrderInfoResult
            {
                Success = true,
                ShopId = data.ShopId,
                ClientId = data.ClientId,
                OrderCode = data.OrderCode,
                ClientOrderCode = data.ClientOrderCode,
                Status = data.Status,
                ToName = data.ToName,
                ToPhone = data.ToPhone,
                ToAddress = data.ToAddress,
                ToWardCode = data.ToWardCode,
                ToDistrictId = data.ToDistrictId,
                FromName = data.FromName,
                FromPhone = data.FromPhone,
                FromAddress = data.FromAddress,
                FromWardCode = data.FromWardCode,
                FromDistrictId = data.FromDistrictId,
                ReturnName = data.ReturnName,
                ReturnPhone = data.ReturnPhone,
                ReturnAddress = data.ReturnAddress,
                ReturnWardCode = data.ReturnWardCode,
                ReturnDistrictId = data.ReturnDistrictId,
                Weight = data.Weight,
                Length = data.Length,
                Width = data.Width,
                Height = data.Height,
                ConvertedWeight = data.ConvertedWeight,
                ServiceTypeId = data.ServiceTypeId,
                ServiceId = data.ServiceId,
                PaymentTypeId = data.PaymentTypeId,
                CodAmount = data.CodAmount,
                CodCollectDate = DateTime.TryParse(data.CodCollectDate, out var codCollectDate) ? codCollectDate : null,
                CodTransferDate = DateTime.TryParse(data.CodTransferDate, out var codTransferDate) ? codTransferDate : null,
                IsCodTransferred = data.IsCodTransferred,
                IsCodCollected = data.IsCodCollected,
                CodFailedAmount = data.CodFailedAmount,
                CodFailedCollectDate = DateTime.TryParse(data.CodFailedCollectDate, out var codFailedDate) ? codFailedDate : null,
                InsuranceValue = data.InsuranceValue,
                OrderValue = data.OrderValue,
                RequiredNote = data.RequiredNote,
                Content = data.Content,
                Note = data.Note,
                EmployeeNote = data.EmployeeNote,
                Coupon = data.Coupon,
                PickStationId = data.PickStationId,
                DeliverStationId = data.DeliverStationId,
                PickWarehouseId = data.PickWarehouseId,
                DeliverWarehouseId = data.DeliverWarehouseId,
                CurrentWarehouseId = data.CurrentWarehouseId,
                ReturnWarehouseId = data.ReturnWarehouseId,
                NextWarehouseId = data.NextWarehouseId,
                LeadTime = DateTime.TryParse(data.LeadTime, out var leadTime) ? leadTime : null,
                OrderDate = DateTime.Parse(data.OrderDate),
                FinishDate = DateTime.TryParse(data.FinishDate, out var finishDate) ? finishDate : null,
                CreatedDate = DateTime.Parse(data.CreatedDate),
                UpdatedDate = DateTime.Parse(data.UpdatedDate),
                Log = data.Log?.Select(log => new GhnStatusLog
                {
                    Status = log.Status,
                    UpdatedDate = DateTime.Parse(log.UpdatedDate)
                }).ToList() ?? new(),
                Tag = data.Tag ?? new()
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting GHN order info for {OrderCode}", orderCode);
            return new GhnOrderInfoResult
            {
                Success = false,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<GhnOrderOperationResult> CancelOrdersAsync(List<string> orderCodes)
    {
        try
        {
            _logger.LogInformation("Cancelling GHN orders: {OrderCodes}", string.Join(", ", orderCodes));

            var payload = new { order_codes = orderCodes };
            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/switch-status/cancel", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            return ProcessOrderOperationResponse(response, responseContent, "cancel");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error cancelling GHN orders: {OrderCodes}", string.Join(", ", orderCodes));
            return new GhnOrderOperationResult
            {
                Success = false,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<GhnOrderOperationResult> ReturnOrdersAsync(List<string> orderCodes)
    {
        try
        {
            _logger.LogInformation("Returning GHN orders: {OrderCodes}", string.Join(", ", orderCodes));

            var payload = new { order_codes = orderCodes };
            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/switch-status/return", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            return ProcessOrderOperationResponse(response, responseContent, "return");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error returning GHN orders: {OrderCodes}", string.Join(", ", orderCodes));
            return new GhnOrderOperationResult
            {
                Success = false,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<Result> UpdateCodAmountAsync(UpdateGhnCodRequest request)
    {
        try
        {
            _logger.LogInformation("Updating COD amount for GHN order {OrderCode} to {CodAmount}",
                request.OrderCode, request.CodAmount);

            var payload = new
            {
                order_code = request.OrderCode,
                cod_amount = request.CodAmount
            };

            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/shipping-order/updateCOD", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("GHN COD update failed: {StatusCode} - {Content}", response.StatusCode, responseContent);
                return Result.Failure(GhnShippingErrors.CodUpdateFailed);
            }

            var ghnResponse = JsonSerializer.Deserialize<GhnApiResponse<object>>(responseContent);

            if (ghnResponse?.Code != 200)
            {
                _logger.LogError("GHN COD update failed: {Message}", ghnResponse?.Message);
                return Result.Failure(GhnShippingErrors.CodUpdateFailed);
            }

            _logger.LogInformation("Successfully updated COD amount for GHN order {OrderCode}", request.OrderCode);
            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating COD amount for GHN order {OrderCode}", request.OrderCode);
            return Result.Failure(GhnShippingErrors.CodUpdateFailed);
        }
    }

    #endregion

    #region Delivery Planning & Estimates

    public async Task<GhnDeliveryTimeResult> CalculateDeliveryTimeAsync(CalculateGhnDeliveryTimeRequest request)
    {
        try
        {
            _logger.LogInformation("Calculating delivery time from district {FromDistrict} to {ToDistrict}",
                request.FromDistrictId, request.ToDistrictId);

            var payload = new
            {
                from_district_id = request.FromDistrictId,
                from_ward_code = request.FromWardCode,
                to_district_id = request.ToDistrictId,
                to_ward_code = request.ToWardCode,
                service_id = request.ServiceId
            };

            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/shipping-order/leadtime", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("GHN delivery time calculation failed: {StatusCode} - {Content}", response.StatusCode, responseContent);
                return new GhnDeliveryTimeResult
                {
                    Success = false,
                    ErrorMessage = $"GHN API error: {response.StatusCode}"
                };
            }

            var ghnResponse = JsonSerializer.Deserialize<GhnApiResponse<GhnDeliveryTimeData>>(responseContent);

            if (ghnResponse?.Code != 200)
            {
                _logger.LogError("GHN delivery time calculation failed: {Message}", ghnResponse?.Message);
                return new GhnDeliveryTimeResult
                {
                    Success = false,
                    ErrorMessage = ghnResponse?.Message ?? "Unknown error"
                };
            }

            return new GhnDeliveryTimeResult
            {
                Success = true,
                LeadTime = ghnResponse.Data.LeadTime,
                OrderDate = ghnResponse.Data.OrderDate
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calculating delivery time from district {FromDistrict} to {ToDistrict}",
                request.FromDistrictId, request.ToDistrictId);
            return new GhnDeliveryTimeResult
            {
                Success = false,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<GhnPickShiftResult> GetPickShiftsAsync()
    {
        try
        {
            _logger.LogInformation("Getting available pick shifts from GHN");

            var response = await _httpClient.GetAsync("/shift/date");
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("GHN pick shifts retrieval failed: {StatusCode} - {Content}", response.StatusCode, responseContent);
                return new GhnPickShiftResult
                {
                    Success = false,
                    ErrorMessage = $"GHN API error: {response.StatusCode}"
                };
            }

            var ghnResponse = JsonSerializer.Deserialize<GhnApiResponse<List<GhnPickShiftData>>>(responseContent);

            if (ghnResponse?.Code != 200)
            {
                _logger.LogError("GHN pick shifts retrieval failed: {Message}", ghnResponse?.Message);
                return new GhnPickShiftResult
                {
                    Success = false,
                    ErrorMessage = ghnResponse?.Message ?? "Unknown error"
                };
            }

            return new GhnPickShiftResult
            {
                Success = true,
                Shifts = ghnResponse.Data?.Select(shift => new GhnPickShift
                {
                    Id = shift.Id,
                    Title = shift.Title,
                    FromTime = shift.FromTime,
                    ToTime = shift.ToTime
                }).ToList() ?? new()
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting GHN pick shifts");
            return new GhnPickShiftResult
            {
                Success = false,
                ErrorMessage = ex.Message
            };
        }
    }

    #endregion

    #region Store/Branch Management

    public async Task<CreateGhnStoreResult> CreateStoreAsync(CreateGhnStoreRequest request)
    {
        try
        {
            _logger.LogInformation("Creating GHN store: {StoreName} at {Address}", request.Name, request.Address);

            var payload = new
            {
                district_id = request.DistrictId,
                ward_code = request.WardCode,
                name = request.Name,
                phone = request.Phone,
                address = request.Address
            };

            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/shop/register", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("GHN store creation failed: {StatusCode} - {Content}", response.StatusCode, responseContent);
                return new CreateGhnStoreResult
                {
                    Success = false,
                    ErrorMessage = $"GHN API error: {response.StatusCode}"
                };
            }

            var ghnResponse = JsonSerializer.Deserialize<GhnApiResponse<GhnCreateStoreData>>(responseContent);

            if (ghnResponse?.Code != 200)
            {
                _logger.LogError("GHN store creation failed: {Message}", ghnResponse?.Message);
                return new CreateGhnStoreResult
                {
                    Success = false,
                    ErrorMessage = ghnResponse?.Message ?? "Unknown error"
                };
            }

            _logger.LogInformation("Successfully created GHN store {StoreName} with shop ID {ShopId}",
                request.Name, ghnResponse.Data.ShopId);

            return new CreateGhnStoreResult
            {
                Success = true,
                ShopId = ghnResponse.Data.ShopId
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating GHN store: {StoreName}", request.Name);
            return new CreateGhnStoreResult
            {
                Success = false,
                ErrorMessage = ex.Message
            };
        }
    }

    #endregion

    #region Utility Methods

    public async Task<Result> ValidateConfigurationAsync()
    {
        try
        {
            _logger.LogInformation("Validating GHN configuration");

            // Test API connectivity by getting pick shifts
            var response = await _httpClient.GetAsync("/shift/date");

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("GHN configuration is valid and API is accessible");
                return Result.Success();
            }
            else
            {
                _logger.LogError("GHN configuration validation failed: {StatusCode}", response.StatusCode);
                return Result.Failure(GhnShippingErrors.ConfigurationInvalid);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error validating GHN configuration");
            return Result.Failure(GhnShippingErrors.ConfigurationInvalid);
        }
    }

    #endregion

    #region Private Helper Methods

    private GhnOrderOperationResult ProcessOrderOperationResponse(HttpResponseMessage response, string responseContent, string operation)
    {
        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError("GHN {Operation} operation failed: {StatusCode} - {Content}", operation, response.StatusCode, responseContent);
            return new GhnOrderOperationResult
            {
                Success = false,
                ErrorMessage = $"GHN API error: {response.StatusCode}"
            };
        }

        var ghnResponse = JsonSerializer.Deserialize<GhnApiResponse<List<GhnOrderOperationData>>>(responseContent);

        if (ghnResponse?.Code != 200)
        {
            _logger.LogError("GHN {Operation} operation failed: {Message}", operation, ghnResponse?.Message);
            return new GhnOrderOperationResult
            {
                Success = false,
                ErrorMessage = ghnResponse?.Message ?? "Unknown error"
            };
        }

        return new GhnOrderOperationResult
        {
            Success = true,
            Results = ghnResponse.Data?.Select(item => new GhnOrderOperationItem
            {
                OrderCode = item.OrderCode,
                Result = item.Result,
                Message = item.Message
            }).ToList() ?? new()
        };
    }

    // Interface method implementations (single order operations)
    public async Task<GhnOrderOperationResult> CancelOrderAsync(string orderCode)
    {
        return await CancelOrdersAsync(new List<string> { orderCode });
    }

    public async Task<CreateGhnOrderResult> CreateReturnOrderAsync(CreateGhnOrderRequest request)
    {
        // For return orders, we typically use the same endpoint but with different parameters
        // This implementation would need to be adjusted based on specific GHN return order requirements
        return await CreateOrderAsync(request);
    }

    public async Task<GhnOrderOperationResult> UpdateCodAsync(UpdateGhnCodRequest request)
    {
        var result = await UpdateCodAmountAsync(request);
        return new GhnOrderOperationResult
        {
            Success = result.IsSuccess,
            ErrorMessage = result.IsFailure ? result.Error.Description : null
        };
    }

    public async Task<GhnDeliveryTimeResult> CalculateExpectedDeliveryTimeAsync(CalculateGhnDeliveryTimeRequest request)
    {
        return await CalculateDeliveryTimeAsync(request);
    }

    public async Task<GhnPickShiftResult> GetPickShiftAsync()
    {
        return await GetPickShiftsAsync();
    }

    #endregion
}

#region GHN API Response Models

/// <summary>
/// Generic GHN API response wrapper
/// </summary>
internal class GhnApiResponse<T>
{
    public int Code { get; set; }
    public string Message { get; set; } = string.Empty;
    public T Data { get; set; } = default!;
}

/// <summary>
/// GHN create order response data
/// </summary>
internal class GhnCreateOrderData
{
    public string OrderCode { get; set; } = string.Empty;
    public string SortCode { get; set; } = string.Empty;
    public string TransType { get; set; } = string.Empty;
    public string DistrictEncode { get; set; } = string.Empty;
    public string WardEncode { get; set; } = string.Empty;
    public string ExpectedDeliveryTime { get; set; } = string.Empty;
    public GhnFeeData Fee { get; set; } = new();
    public int TotalFee { get; set; }
}

/// <summary>
/// GHN fee breakdown
/// </summary>
internal class GhnFeeData
{
    public int MainService { get; set; }
    public int Insurance { get; set; }
    public int StationDo { get; set; }
    public int StationPu { get; set; }
    public int Return { get; set; }
    public int R2S { get; set; }
    public int Coupon { get; set; }
}

/// <summary>
/// GHN order information response data
/// </summary>
internal class GhnOrderInfoData
{
    public int ShopId { get; set; }
    public int ClientId { get; set; }
    public string OrderCode { get; set; } = string.Empty;
    public string ClientOrderCode { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string ToName { get; set; } = string.Empty;
    public string ToPhone { get; set; } = string.Empty;
    public string ToAddress { get; set; } = string.Empty;
    public string ToWardCode { get; set; } = string.Empty;
    public int ToDistrictId { get; set; }
    public string FromName { get; set; } = string.Empty;
    public string FromPhone { get; set; } = string.Empty;
    public string FromAddress { get; set; } = string.Empty;
    public string FromWardCode { get; set; } = string.Empty;
    public int FromDistrictId { get; set; }
    public string? ReturnName { get; set; }
    public string? ReturnPhone { get; set; }
    public string? ReturnAddress { get; set; }
    public string? ReturnWardCode { get; set; }
    public int? ReturnDistrictId { get; set; }
    public int Weight { get; set; }
    public int Length { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public int ConvertedWeight { get; set; }
    public int ServiceTypeId { get; set; }
    public int ServiceId { get; set; }
    public int PaymentTypeId { get; set; }
    public int CodAmount { get; set; }
    public string? CodCollectDate { get; set; }
    public string? CodTransferDate { get; set; }
    public bool IsCodTransferred { get; set; }
    public bool IsCodCollected { get; set; }
    public int CodFailedAmount { get; set; }
    public string? CodFailedCollectDate { get; set; }
    public int InsuranceValue { get; set; }
    public int OrderValue { get; set; }
    public string RequiredNote { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string? Note { get; set; }
    public string? EmployeeNote { get; set; }
    public string? Coupon { get; set; }
    public int PickStationId { get; set; }
    public int DeliverStationId { get; set; }
    public int PickWarehouseId { get; set; }
    public int DeliverWarehouseId { get; set; }
    public int CurrentWarehouseId { get; set; }
    public int ReturnWarehouseId { get; set; }
    public int NextWarehouseId { get; set; }
    public string? LeadTime { get; set; }
    public string OrderDate { get; set; } = string.Empty;
    public string? FinishDate { get; set; }
    public string CreatedDate { get; set; } = string.Empty;
    public string UpdatedDate { get; set; } = string.Empty;
    public List<GhnStatusLogData>? Log { get; set; }
    public List<string>? Tag { get; set; }
}

/// <summary>
/// GHN status log entry
/// </summary>
internal class GhnStatusLogData
{
    public string Status { get; set; } = string.Empty;
    public string UpdatedDate { get; set; } = string.Empty;
}

/// <summary>
/// GHN delivery time calculation data
/// </summary>
internal class GhnDeliveryTimeData
{
    public long LeadTime { get; set; }
    public long OrderDate { get; set; }
}

/// <summary>
/// GHN pick shift data
/// </summary>
internal class GhnPickShiftData
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int FromTime { get; set; }
    public int ToTime { get; set; }
}

/// <summary>
/// GHN store creation response data
/// </summary>
internal class GhnCreateStoreData
{
    public int ShopId { get; set; }
}

/// <summary>
/// GHN order operation response item
/// </summary>
internal class GhnOrderOperationData
{
    public string OrderCode { get; set; } = string.Empty;
    public bool Result { get; set; }
    public string Message { get; set; } = string.Empty;
}

#endregion
