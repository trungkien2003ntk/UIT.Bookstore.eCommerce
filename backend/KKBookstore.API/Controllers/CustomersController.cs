using AutoMapper;
using KKBookstore.Abstractions;
using KKBookstore.Features.Customers.BlockCustomer;
using KKBookstore.Features.Customers.GetCustomerDetail;
using KKBookstore.Features.Customers.GetCustomerList;
using KKBookstore.Features.Customers.GetCustomerStatusSummary;
using KKBookstore.Features.Customers.UnblockCustomer;
using KKBookstore.Features.Customers.UpdateCustomer;
using KKBookstore.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KKBookstore.Controllers;

[Route("api/customers")]
public class CustomersController(
    ISender sender,
    IMapper mapper
) : ApiController(sender)
{
    [HttpGet]
    public async Task<IActionResult> GetCustomers(
        [FromQuery] GetCustomerListQuery query,
        CancellationToken cancellationToken = default
    )
    {
        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomerDetail(
        [FromRoute] int id,
        CancellationToken cancellationToken = default
    )
    {
        var result = await Sender.Send(new GetCustomerDetailQuery(id), cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCustomer(
        [FromRoute] int id,
        [FromBody] UpdateCustomerCommand command,
        CancellationToken cancellationToken = default
    )
    {
        if (id != command.Id)
        {
            var resultTemp = Result.Failure(Error.Validation("Endpoint.InvalidRequest", "Customer id in request doesn't match with the id in the route"));
            return ToActionResult(resultTemp);
        }

        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    [HttpPost("{id}/block")]
    public async Task<IActionResult> BlockCustomer(
        [FromRoute] int id,
        CancellationToken cancellationToken = default
    )
    {
        var result = await Sender.Send(new BlockCustomerCommand(id), cancellationToken);

        return result.IsSuccess ? Ok() : ToActionResult(result);
    }

    [HttpPost("{id}/unblock")]
    public async Task<IActionResult> UnblockCustomer(
        [FromRoute] int id,
        CancellationToken cancellationToken = default
    )
    {
        var result = await Sender.Send(new UnblockCustomerCommand(id), cancellationToken);

        return result.IsSuccess ? Ok() : ToActionResult(result);
    }

    [HttpGet("status-summary")]
    public async Task<IActionResult> GetCustomerStatusSummary(
        CancellationToken cancellationToken = default
    )
    {
        var result = await Sender.Send(new GetCustomerStatusSummaryQuery(), cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }
}
