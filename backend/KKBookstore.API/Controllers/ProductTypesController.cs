﻿
using KKBookstore.API.Abstractions;
using KKBookstore.Application.Features.ProductTypes.GetProductTypeAttributes;
using KKBookstore.Application.Features.ProductTypes.GetProductTypeDetail;
using KKBookstore.Application.Features.ProductTypes.GetProductTypeList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KKBookstore.API.Controllers;

[Route("api/product-types")]
public class ProductTypesController(
    ISender sender
) : ApiController(sender)
{
    [HttpGet]
    public async Task<IActionResult> GetProductTypes(
        [FromQuery] GetProductTypeListQuery query,
        CancellationToken cancellationToken = default
    )
    {
        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductTypeDetail(
        int id,
        [FromQuery] bool withParent = true,
        CancellationToken cancellationToken = default
    )
    {
        var result = await Sender.Send(new GetProductTypeDetailQuery(id, withParent), cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    // get product attributes of a product type
    [HttpGet("{id}/attributes")]
    public async Task<IActionResult> GetProductTypeAttributes(
        int id,
        CancellationToken cancellationToken = default
    )
    {
        var query = new GetProductTypeAttributesQuery(id);

        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }
}
