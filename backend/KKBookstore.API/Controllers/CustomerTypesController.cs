using KKBookstore.Abstractions;
using KKBookstore.Features.CustomerTypes.CreateCustomerType;
using KKBookstore.Features.CustomerTypes.DeleteCustomerType;
using KKBookstore.Features.CustomerTypes.GetCustomerTypeDetail;
using KKBookstore.Features.CustomerTypes.GetCustomerTypeList;
using KKBookstore.Features.CustomerTypes.UpdateCustomerType;
using KKBookstore.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KKBookstore.Controllers;

[Route("api/customer-types")]
public class CustomerTypesController(
    ISender sender
) : ApiController(sender)
{
    [HttpGet]
    public async Task<IActionResult> GetCustomerTypes(
        [FromQuery] GetCustomerTypeListQuery query,
        CancellationToken cancellationToken = default
    )
    {
        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomerTypeDetail(
        [FromRoute] int id,
        CancellationToken cancellationToken = default
    )
    {
        var result = await Sender.Send(new GetCustomerTypeDetailQuery(id), cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomerType(
        [FromBody] CreateCustomerTypeCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCustomerType(
        [FromRoute] int id,
        [FromBody] UpdateCustomerTypeCommand command,
        CancellationToken cancellationToken = default
    )
    {
        if (id != command.Id)
        {
            var resultTemp = Result.Failure(Error.Validation("Endpoint.InvalidRequest", "CustomerType id in request doesn't match with the id in the route"));
            return ToActionResult(resultTemp);
        }

        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomerType(
        [FromRoute] int id,
        CancellationToken cancellationToken = default
    )
    {
        var result = await Sender.Send(new DeleteCustomerTypeCommand(id), cancellationToken);

        return result.IsSuccess ? Ok() : ToActionResult(result);
    }
}
