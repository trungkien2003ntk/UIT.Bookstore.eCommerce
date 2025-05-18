using KKBookstore.Abstractions;
using KKBookstore.Constants;
using KKBookstore.Features.StockTransfers.CreateStockTransfer;
using KKBookstore.Features.StockTransfers.DeleteStockTransfer;
using KKBookstore.Features.StockTransfers.GetStockTransferDetail;
using KKBookstore.Features.StockTransfers.GetStockTransferList;
using KKBookstore.Features.StockTransfers.UpdateStockTransfer;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KKBookstore.Controllers;

[Route("api/stock-transfers")]
[Authorize(Roles = $"{Role.Admin},{Role.SalesStaff}")]
public class StockTransfersController(
    ISender sender
) : ApiController(sender)
{
    [HttpGet]
    public async Task<IActionResult> GetStockTransfers(
        [FromQuery] GetStockTransferListQuery query,
        CancellationToken cancellationToken = default
    )
    {
        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetStockTransferDetail(
        [FromRoute] int id,
        CancellationToken cancellationToken = default
    )
    {
        var result = await Sender.Send(new GetStockTransferDetailQuery(id), cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateStockTransfer(
        [FromBody] CreateStockTransferCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess 
            ? CreatedAtAction(nameof(GetStockTransferDetail), new { id = result.Value.Id }, result.Value) 
            : ToActionResult(result);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStockTransfer(
        [FromRoute] int id,
        [FromBody] UpdateStockTransferCommand command,
        CancellationToken cancellationToken = default
    )
    {
        if (id != command.Id)
        {
            return BadRequest("Id in route must match Id in body");
        }
        
        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStockTransfer(
        [FromRoute] int id,
        CancellationToken cancellationToken = default
    )
    {
        var result = await Sender.Send(new DeleteStockTransferCommand(id), cancellationToken);

        return result.IsSuccess ? NoContent() : ToActionResult(result);
    }
}
