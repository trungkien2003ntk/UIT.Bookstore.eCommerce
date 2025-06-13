using KKBookstore.Abstractions;
using KKBookstore.Features.DiscountVouchers.CreateDiscountVoucher;
using KKBookstore.Features.DiscountVouchers.DeleteDiscountVoucher;
using KKBookstore.Features.DiscountVouchers.DiscountVoucherAction;
using KKBookstore.Features.DiscountVouchers.GetDiscountVoucherDetail;
using KKBookstore.Features.DiscountVouchers.GetDiscountVoucherList;
using KKBookstore.Features.DiscountVouchers.UpdateDiscountVoucher;
using KKBookstore.Orders;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KKBookstore.Controllers;

[Route("api/discount-vouchers")]
public class DiscountVoucherController(
    ISender sender
) : ApiController(sender)
{
    [HttpGet]
    public async Task<IActionResult> GetDiscountVouchers(
        [FromQuery] GetDiscountVoucherListQuery query,
        CancellationToken cancellationToken = default
    )
    {
        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDiscountVoucherDetail(
        [FromRoute] int id,
        CancellationToken cancellationToken = default
    )
    {
        var result = await Sender.Send(new GetDiscountVoucherDetailQuery(id), cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }
    [HttpPost]
    public async Task<IActionResult> CreateDiscountVoucher(
        [FromBody] CreateDiscountVoucherCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDiscountVoucher(
        [FromRoute] int id,
        [FromBody] UpdateDiscountVoucherCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var commandWithId = command with { Id = id };
        var result = await Sender.Send(commandWithId, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    [HttpPatch("{id}/action")]
    public async Task<IActionResult> DiscountVoucherAction(
        [FromRoute] int id,
        [FromBody] DiscountVoucherActionRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var command = new DiscountVoucherActionCommand
        {
            Id = id,
            Action = request.Action
        };

        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok() : ToActionResult(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDiscountVoucher(
        [FromRoute] int id,
        CancellationToken cancellationToken = default
    )
    {
        var result = await Sender.Send(new DeleteDiscountVoucherCommand(id), cancellationToken);

        return result.IsSuccess ? Ok() : ToActionResult(result);
    }
}

public record DiscountVoucherActionRequest
{
    public DiscountVoucherActionType Action { get; init; }
}
