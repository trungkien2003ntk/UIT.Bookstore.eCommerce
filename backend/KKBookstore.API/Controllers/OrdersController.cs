using AutoMapper;
using KKBookstore.Abstractions;
using KKBookstore.Features.Checkout.PlaceOrder;
using KKBookstore.Features.Orders.GetOrderDetail;
using KKBookstore.Features.Orders.GetOrderList;
using KKBookstore.Features.Orders.SendOrderEmail;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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

    [HttpPost("place-order")]
    public async Task<IActionResult> PlaceOrderAsync(
        [FromBody] PlaceOrderCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? CreatedAtAction(nameof(GetOrderAsync), new { id = result.Value!.OrderId }, result.Value) : ToActionResult(result);
    }
}
