using AutoMapper;
using KKBookstore.API.Abstractions;
using KKBookstore.API.Contracts.Requests;
using KKBookstore.Application.Features.Products.CreateProduct;
using KKBookstore.Application.Features.Products.GetProductDetail;
using KKBookstore.Application.Features.Products.GetProductList;
using KKBookstore.Application.Features.Products.GetProductRatingList;
using KKBookstore.Application.Features.Products.GetTrendyProductList;
using KKBookstore.Application.Features.Products.GetUnitMeasures;
using KKBookstore.Application.Features.Products.SearchProducts;
using KKBookstore.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KKBookstore.API.Controllers;


[Route("api/products")]
public class ProductsController(
    ISender sender,
    IMapper mapper
) : ApiController(sender)
{
    [HttpGet]
    public async Task<IActionResult> GetProducts(
        [FromQuery] GetProductListRequest filter,
        CancellationToken cancellationToken = default
    )
    {
        var query = mapper.Map<GetProductListQuery>(filter);

        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductDetail(int id)
    {
        var result = await Sender.Send(new GetProductDetailQuery(id));

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    [HttpGet("trendy")]
    public async Task<IActionResult> GetTrendyProducts(
        [FromQuery] GetTrendyProductListQuery query,
        CancellationToken cancellationToken = default
    )
    {
        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    [HttpGet("{id}/ratings")]
    public async Task<IActionResult> GetProductRatings(
        [FromRoute] int id,
        [FromQuery] GetProductRatingListRequest request,
        CancellationToken cancellationToken = default
    )
    {
        request.ProductId ??= id;

        if (request.ProductId.Value != id)
        {
            var resultTemp = Result.Failure(Error.Validation("Endpoint.InvalidRequest", "Product id in request doesn't match with the id in the route"));
            return ToActionResult(resultTemp);
        }

        var query = mapper.Map<GetProductRatingListQuery>(request);

        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchProducts(
        [FromQuery] SearchProductsQuery query,
        CancellationToken cancellationToken = default
    )
    {
        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }


    [HttpGet("unit-measures")]
    public async Task<IActionResult> GetUnitMeasures(
        CancellationToken cancellationToken = default
    )
    {
        var result = await Sender.Send(new GetUnitMeasuresQuery(), cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProductAsync(
        [FromBody] CreateProductCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }
}
