using KKBookstore.Abstractions;
using KKBookstore.Constants;
using KKBookstore.Features.StockAdjustments.CreateStockAdjustment;
using KKBookstore.Features.StockAdjustments.DeleteStockAdjustment;
using KKBookstore.Features.StockAdjustments.GetStockAdjustmentDetail;
using KKBookstore.Features.StockAdjustments.GetStockAdjustmentList;
using KKBookstore.Features.StockAdjustments.GetStockAdjustmentSummary;
using KKBookstore.Features.StockAdjustments.UpdateStockAdjustment;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KKBookstore.Controllers;

[Route("api/stock-adjustments")]
[Authorize(Roles = $"{Role.Admin},{Role.SalesStaff}")]
public class StockAdjustmentsController(
    ISender sender
) : ApiController(sender)
{
    [HttpGet]
    public async Task<IActionResult> GetStockAdjustments(
        [FromQuery] GetStockAdjustmentListQuery query,
        CancellationToken cancellationToken = default
    )
    {
        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }
    [HttpGet("summary")]
    public async Task<IActionResult> GetStockAdjustmentSummary(
      [FromQuery] GetStockAdjustmentSummaryQuery query,
      CancellationToken cancellationToken = default
  )
    {
        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetStockAdjustmentDetail(
        [FromRoute] int id,
        CancellationToken cancellationToken = default
    )
    {
        var result = await Sender.Send(new GetStockAdjustmentDetailQuery(id), cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }
    [HttpPost]
    public async Task<IActionResult> CreateStockAdjustment(
        [FromBody] CreateStockAdjustmentCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess
            ? CreatedAtAction(nameof(GetStockAdjustmentDetail), new { id = result.Value.Id }, result.Value)
            : ToActionResult(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStockAdjustment(
        [FromRoute] int id,
        [FromBody] UpdateStockAdjustmentCommand command,
        CancellationToken cancellationToken = default
    )
    {
        if (id != command.Id)
        {
            return BadRequest("Id in route must match Id in body");
        }

        var result = await Sender.Send(command, cancellationToken); return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStockAdjustment(
        [FromRoute] int id,
        CancellationToken cancellationToken = default
    )
    {
        var result = await Sender.Send(new DeleteStockAdjustmentCommand(id), cancellationToken);

        return result.IsSuccess ? NoContent() : ToActionResult(result);
    }
}