using AutoMapper;
using KKBookstore.Abstractions;
using KKBookstore.Contracts.Requests;
using KKBookstore.Features.Checkout.PlaceOrder;
using KKBookstore.Features.Orders.GetOrderDetail;
using KKBookstore.Features.Orders.GetOrderList;
using KKBookstore.Features.Orders.SendOrderEmail;
using KKBookstore.Features.Orders.SelectBranchForPackaging;
using KKBookstore.Features.Orders.ConfirmPackagingComplete;
using KKBookstore.Features.Orders.UpdateOrderStatus;
using KKBookstore.Features.Orders.ConfirmOrderReceived;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace KKBookstore.Controllers;

[Route("api/orders")]
public class OrdersController(
    ISender sender,
    IMapper mapper
) : ApiController(sender)
{
    // get orders
    [HttpGet]
    public async Task<IActionResult> GetOrdersAsync(
        [FromQuery] Contracts.Requests.GetOrderListRequest filter,
        CancellationToken cancellationToken = default)
    {
        var query = mapper.Map<GetOrderListQuery>(filter);

        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    // get order
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var result = await Sender.Send(new GetOrderDetailQuery(id), cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }    // send email for order
    [HttpPost("send-email")]
    public async Task<IActionResult> SendEmailAsync(
        SendOrderEmailCommand command,
        CancellationToken cancellationToken = default)
    {
        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok() : ToActionResult(result);
    }

    // Admin Actions
      /// <summary>
    /// Admin selects a branch to handle packaging for an order
    /// </summary>
    [HttpPatch("{id}/select-branch-for-packaging")]
    [Authorize] // Add proper admin authorization when available
    public async Task<IActionResult> SelectBranchForPackagingAsync(
        int id,
        [FromBody] SelectBranchForPackagingRequest request,
        CancellationToken cancellationToken = default)
    {        var adminUserId = User.Identity?.IsAuthenticated == true 
            ? int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0")
            : (int?)null;

        var command = new SelectBranchForPackagingCommand
        {
            OrderId = id,
            BranchId = request.BranchId,
            Notes = request.Notes,
            AdminUserId = adminUserId
        };

        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok() : ToActionResult(result);
    }    /// <summary>
    /// Branch admin confirms packaging is complete and creates shipping order
    /// </summary>
    [HttpPatch("{id}/confirm-packaging-complete")]
    [Authorize] // Add proper admin authorization when available
    public async Task<IActionResult> ConfirmPackagingCompleteAsync(
        int id,
        [FromBody] ConfirmPackagingCompleteRequest request,
        CancellationToken cancellationToken = default)
    {
        var adminUserId = User.Identity?.IsAuthenticated == true 
            ? int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0")
            : (int?)null;

        var command = new ConfirmPackagingCompleteCommand
        {
            OrderId = id,
            BranchId = request.BranchId,
            Notes = request.Notes,
            AdminUserId = adminUserId
        };

        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }    /// <summary>
    /// Admin manually updates order status (backup endpoint)
    /// </summary>
    [HttpPatch("{id}/update-status")]
    [Authorize] // Add proper admin authorization when available
    public async Task<IActionResult> UpdateOrderStatusAsync(
        int id,
        [FromBody] UpdateOrderStatusRequest request,
        CancellationToken cancellationToken = default)
    {
        var adminUserId = User.Identity?.IsAuthenticated == true 
            ? int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0")
            : (int?)null;

        var command = new UpdateOrderStatusCommand
        {
            OrderId = id,
            Status = request.Status,
            Reason = request.Reason,
            Notes = request.Notes,
            AdminUserId = adminUserId
        };

        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok() : ToActionResult(result);
    }

    // Customer Actions

    /// <summary>
    /// Customer confirms they have received the order
    /// </summary>
    [HttpPatch("{id}/confirm-received")]
    [Authorize]
    public async Task<IActionResult> ConfirmOrderReceivedAsync(
        int id,
        [FromBody] ConfirmOrderReceivedRequest request,
        CancellationToken cancellationToken = default)
    {
        var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!);

        var command = new ConfirmOrderReceivedCommand
        {
            OrderId = id,
            CustomerId = userId,
            FeedbackNotes = request.FeedbackNotes,
            Rating = request.Rating
        };

        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok() : ToActionResult(result);
    }
}
