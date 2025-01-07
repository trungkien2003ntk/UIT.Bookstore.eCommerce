﻿using AutoMapper;
using KKBookstore.API.Abstractions;
using KKBookstore.Application.Features.Orders.GetOrderDetail;
using KKBookstore.Application.Features.Orders.SendOrderEmail;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KKBookstore.API.Controllers;

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
        var query = mapper.Map<Application.Features.Orders.GetOrderList.GetOrderListQuery>(filter);

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
    }

    // send email for order
    [HttpPost("send-email")]
    public async Task<IActionResult> SendEmailAsync(
        SendOrderEmailCommand command,
        CancellationToken cancellationToken = default)
    {
        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok() : ToActionResult(result);
    }

    //[HttpPost("place-order")]
    //public async Task<IActionResult> PlaceOrderAsync(
    //    [FromBody] PlaceOrderCommand command,
    //    CancellationToken cancellationToken = default
    //)
    //{
    //    var result = await Sender.Send(command, cancellationToken);

    //    return result.IsSuccess ? CreatedAtAction(nameof(GetOrderAsync), new { id = result.Value }, result.Value) : ToActionResult(result);
    //}
}
