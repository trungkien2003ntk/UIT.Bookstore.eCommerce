using KKBookstore.API.Abstractions;
using KKBookstore.API.Contracts.Requests;
using KKBookstore.API.Extensions;
using KKBookstore.Application.Features.Checkout.Confirm;
using KKBookstore.Application.Features.Checkout.HandleIPN;
using KKBookstore.Application.Features.Checkout.PlaceOrder;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KKBookstore.API.Controllers;

[Authorize]
[Route("api/[controller]")]
public class CheckoutController(ISender sender) : ApiController(sender)
{

    [HttpPost("confirm")]
    public async Task<IActionResult> CheckoutAsync(
        [FromBody] ConfirmCheckoutRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!);

        var query = new ConfirmCheckoutQuery
        {
            UserId = userId,
            ItemIds = request.ItemIds
        };

        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    [HttpPost("place-order")]
    public async Task<IActionResult> PlaceOrderAsync(
        [FromBody] PlaceOrderRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!);

        var command = new PlaceOrderCommand
        {
            UserId = userId,
            ItemIds = request.ItemIds,
            ShippingAddressId = request.ShippingAddressId,
            PaymentMethodId = request.PaymentMethodId,
            DeliveryMethodId = request.DeliveryMethodId,
            DiscountVoucherId = request.DiscountVoucherId,
            ShippingVoucherId = request.ShippingVoucherId,
            ExpectedDeliveryWhen = request.ExpectedDeliveryWhen,
            Note = request.Note,
            PaymentReturnUrl = request.PaymentReturnUrl,
            IpAddress = HttpContext.GetIpAddress()
        };

        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    [HttpGet("IPN")]
    public async Task<IActionResult> HandleIPNAsync(
        [FromQuery] HandleIPNRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new HandleIPNCommand
        {
            TmnCode = request.TmnCode,
            Amount = request.Amount.Value,
            BankCode = request.BankCode,
            BankTranNo = request.BankTranNo,
            CardType = request.CardType,
            PayDate = request.PayDate,
            OrderInfo = request.OrderInfo,
            TransactionNo = request.TransactionNo.Value,
            ResponseCode = request.ResponseCode,
            TransactionStatus = request.TransactionStatus,
            TxnRef = request.TxnRef,
            SecureHashType = request.SecureHashType,
            SecureHash = request.SecureHash
        };

        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }
}
