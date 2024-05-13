using KKBookstore.API.Extensions;
using KKBookstore.Application.Products.Queries.GetProductDetail;
using KKBookstore.Application.Products.Queries.GetProductList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KKBookstore.API.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ProductsController(
    IMediator mediator
) : Controller
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetProducts(
        [FromQuery]GetProductListQuery query,
        CancellationToken cancellationToken = default
    )
    {
        var result = await _mediator.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToActionResult();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductDetail(int id)
    {
        var result = await _mediator.Send(new GetProductDetailQuery(id));

        return result.IsSuccess ? Ok(result.Value) : result.ToActionResult();
    }

    [HttpPost]
    public async Task<IActionResult> CreateProductAsync(
        
        )
    {

        return Created();
    }
}
