using KKBookstore.Abstractions;
using KKBookstore.Common.Interfaces;
using KKBookstore.Application.Common.Models.RequestDtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace KKBookstore.Controllers;

/// <summary>
/// Controller for handling GHN webhook callbacks.
/// Receives status updates from GHN delivery service and processes order status changes.
/// </summary>
[Route("api/webhooks/ghn")]
public class GhnWebhookController(
    ISender sender,
    IGhnShippingService ghnShippingService,
    ILogger<GhnWebhookController> logger) : ApiController(sender)
{
    private readonly IGhnShippingService _ghnShippingService = ghnShippingService;
    private readonly ILogger<GhnWebhookController> _logger = logger;

    /// <summary>
    /// Receives GHN webhook callbacks for order status updates.
    /// This endpoint is called by GHN when there are updates to order delivery status.
    /// </summary>
    /// <param name="payload">The webhook payload from GHN containing order status information</param>
    /// <returns>200 OK if processed successfully, error response if failed</returns>
    [HttpPost]
    public async Task<IActionResult> ReceiveWebhook([FromBody] GhnWebhookPayload payload)
    {
        try
        {
            _logger.LogInformation(
                "Received GHN webhook for order {OrderCode} with status {Status}. Type: {Type}",
                payload.OrderCode, 
                payload.Status, 
                payload.Type);

            // Process different webhook types
            switch (payload.Type.ToLowerInvariant())
            {
                case "create":
                    _logger.LogInformation("Order {OrderCode} was created in GHN system", payload.OrderCode);
                    break;

                case "switch_status":
                case "update_status":
                    // Process status update
                    var result = await _ghnShippingService.ProcessOrderStatusUpdateAsync(
                        payload.OrderCode, 
                        payload.Status, 
                        payload.Reason);

                    if (result.IsFailure)
                    {
                        _logger.LogError(
                            "Failed to process GHN status update for order {OrderCode}. Error: {Error}",
                            payload.OrderCode, 
                            result.Error.Description);
                        
                        // Still return 200 to prevent GHN from retrying
                        return Ok(new { message = "Received but processing failed", error = result.Error.Description });
                    }

                    _logger.LogInformation(
                        "Successfully processed GHN status update for order {OrderCode} to status {Status}",
                        payload.OrderCode, 
                        payload.Status);
                    break;

                case "update_weight":
                    _logger.LogInformation("Order {OrderCode} weight was updated to {Weight}g", 
                        payload.OrderCode, payload.Weight);
                    break;

                case "update_cod":
                    _logger.LogInformation("Order {OrderCode} COD amount was updated to {CODAmount}", 
                        payload.OrderCode, payload.CODAmount);
                    break;

                case "update_fee":
                    _logger.LogInformation("Order {OrderCode} shipping fee was updated to {TotalFee}", 
                        payload.OrderCode, payload.TotalFee);
                    break;

                default:
                    _logger.LogWarning("Unknown GHN webhook type: {Type} for order {OrderCode}", 
                        payload.Type, payload.OrderCode);
                    break;
            }

            // Return 200 to acknowledge receipt
            return Ok(new { 
                message = "Webhook processed successfully", 
                orderCode = payload.OrderCode,
                status = payload.Status,
                timestamp = DateTime.UtcNow
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, 
                "Unexpected error processing GHN webhook for order {OrderCode}", 
                payload.OrderCode);

            // Still return 200 to prevent GHN from retrying
            return Ok(new { message = "Received but processing failed", error = "Internal server error" });
        }
    }    /// <summary>
    /// Health check endpoint for GHN webhook configuration.
    /// Can be used to verify that the webhook endpoint is accessible.
    /// </summary>
    [HttpGet("health")]
    public IActionResult Health()
    {
        return Ok(new { 
            status = "healthy", 
            service = "ghn-webhook",
            timestamp = DateTime.UtcNow
        });
    }

    /// <summary>
    /// Manual endpoint to simulate GHN webhook calls for testing and manual order status updates.
    /// This endpoint allows admins to manually trigger order status updates when GHN webhook integration is not available.
    /// </summary>
    /// <param name="payload">The webhook payload simulating GHN webhook data</param>
    /// <returns>200 OK if processed successfully, error response if failed</returns>
    [HttpPost("manual-trigger")]
    [Authorize] // Add proper admin authorization when available
    public async Task<IActionResult> ManualTriggerWebhook([FromBody] GhnWebhookPayload payload)
    {
        try
        {
            var adminUserId = User.Identity?.IsAuthenticated == true 
                ? int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0")
                : (int?)null;

            _logger.LogInformation(
                "Manual GHN webhook trigger by admin {AdminUserId} for order {OrderCode} with status {Status}. Type: {Type}",
                adminUserId,
                payload.OrderCode, 
                payload.Status, 
                payload.Type);

            // Process different webhook types
            switch (payload.Type.ToLowerInvariant())
            {
                case "create":
                    _logger.LogInformation("Order {OrderCode} was manually marked as created in GHN system", payload.OrderCode);
                    break;                case "switch_status":
                case "update_status":
                    // Process status update with admin context for manual triggers
                    var result = await _ghnShippingService.ProcessOrderStatusUpdateAsync(
                        payload.OrderCode, 
                        payload.Status, 
                        payload.Reason,
                        adminUserId);

                    if (result.IsFailure)
                    {
                        _logger.LogError(
                            "Failed to process manual GHN status update for order {OrderCode}. Error: {Error}. Triggered by admin {AdminUserId}",
                            payload.OrderCode, 
                            result.Error.Description,
                            adminUserId);
                        
                        return BadRequest(new { 
                            message = "Failed to process manual webhook trigger", 
                            error = result.Error.Description 
                        });
                    }

                    _logger.LogInformation(
                        "Successfully processed manual GHN status update for order {OrderCode} to status {Status}. Triggered by admin {AdminUserId}",
                        payload.OrderCode, 
                        payload.Status,
                        adminUserId);
                    break;

                case "update_weight":
                    _logger.LogInformation("Order {OrderCode} weight was manually updated to {Weight}g", 
                        payload.OrderCode, payload.Weight);
                    break;

                case "update_cod":
                    _logger.LogInformation("Order {OrderCode} COD amount was manually updated to {CODAmount}", 
                        payload.OrderCode, payload.CODAmount);
                    break;

                case "update_fee":
                    _logger.LogInformation("Order {OrderCode} shipping fee was manually updated to {TotalFee}", 
                        payload.OrderCode, payload.TotalFee);
                    break;

                default:
                    _logger.LogWarning("Unknown manual GHN webhook type: {Type} for order {OrderCode}", 
                        payload.Type, payload.OrderCode);
                    break;
            }

            // Return success response
            return Ok(new { 
                message = "Manual webhook trigger processed successfully", 
                orderCode = payload.OrderCode,
                status = payload.Status,
                triggeredBy = adminUserId,
                timestamp = DateTime.UtcNow
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, 
                "Unexpected error processing manual GHN webhook trigger for order {OrderCode}", 
                payload.OrderCode);

            return StatusCode(500, new { 
                message = "Failed to process manual webhook trigger", 
                error = "Internal server error" 
            });
        }
    }
}
