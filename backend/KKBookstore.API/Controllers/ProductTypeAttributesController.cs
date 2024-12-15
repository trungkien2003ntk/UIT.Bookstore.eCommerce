using KKBookstore.API.Abstractions;
using KKBookstore.Application.Features.ProductTypeAttributes.CreateProductAttributeValue;
using KKBookstore.Application.Features.ProductTypeAttributes.DeleteProductAttributeValue;
using KKBookstore.Application.Features.ProductTypeAttributes.GetProductTypeAttributeValues;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KKBookstore.API.Controllers;

[Route("api/product-types-attributes")]
public class ProductTypeAttributesController(
        ISender sender
) : ApiController(sender)
{
    [HttpGet("{id}/values")]
    public async Task<IActionResult> GetProductTypeAttributeValues(
        int id,
        [FromQuery] GetProductTypeAttributeValuesQuery query,
        CancellationToken cancellationToken = default
    )
    {
        query = query with { ProductTypeAttributeId = id };
        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    [HttpPost("{id}/values")]
    public async Task<IActionResult> CreateProductTypeAttributeValue(
        int id,
        [FromBody] CreateProductTypeAttributeValueCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var result = await Sender.Send(command with { ProductTypeAttributeId = id }, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    [HttpDelete("{id}/values/{valueId}")]
    public async Task<IActionResult> DeleteProductTypeAttributeValue(
        int id,
        int valueId,
        CancellationToken cancellationToken = default
    )
    {
        var command = new DeleteProductAttributeValueCommand(id, valueId);
        var result = await Sender.Send(command, cancellationToken);
        return result.IsSuccess ? NoContent() : ToActionResult(result);
    }
}
