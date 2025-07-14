using AutoMapper;
using KKBookstore.Abstractions;
using KKBookstore.Constants;
using KKBookstore.Contracts.Requests;
using KKBookstore.Contracts.Requests.Products;
using KKBookstore.Features.Products.CreateProduct;
using KKBookstore.Features.Products.CreateProductRating;
using KKBookstore.Features.Products.DeleteProduct;
using KKBookstore.Features.Products.GetAdminProductDetail;
using KKBookstore.Features.Products.GetCustomerProductDetail;
using KKBookstore.Features.Products.GetMonthlyTopSellingProductList;
using KKBookstore.Features.Products.GetProductList;
using KKBookstore.Features.Products.GetProductOptions;
using KKBookstore.Features.Products.GetProductRatingList;
using KKBookstore.Features.Products.GetRelatedProductsById;
using KKBookstore.Features.Products.GetRelatedProductsByImage;
using KKBookstore.Features.Products.GetTrendyProductList;
using KKBookstore.Features.Products.GetUnitMeasures;
using KKBookstore.Features.Products.GetWeeklyTopSellingProductList;
using KKBookstore.Features.Products.GetProductsBySentiment;
using KKBookstore.Application.Features.Products.GetProductSentiment;
using KKBookstore.Features.Products.LikeProductRating;
using KKBookstore.Features.Products.ReportProductRating;
using KKBookstore.Features.Products.SearchProducts;
using KKBookstore.Features.Products.UpdateProduct;
using KKBookstore.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KKBookstore.Controllers;


[Route("api/products")]
public class ProductsController(
    ISender sender,
    IMapper mapper
) : ApiController(sender)
{
    [HttpPost("list")]
    public async Task<IActionResult> GetProducts(
        [FromBody] GetProductListRequest filter,
        CancellationToken cancellationToken = default
    )
    {
        var query = mapper.Map<GetProductListQuery>(filter);

        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    [HttpGet("top-selling/weekly")]
    public async Task<IActionResult> GetTopSellingProducts(
        [FromQuery] GetWeeklyTopSellingProductListQuery query,
        CancellationToken cancellationToken = default
    )
    {
        var result = await Sender.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    [HttpGet("top-selling/monthly")]
    public async Task<IActionResult> GetMonthlyTopSellingProducts(
        [FromQuery] GetMonthlyTopSellingProductListQuery query,
        CancellationToken cancellationToken = default
    )
    {
        var result = await Sender.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductDetail(int id)
    {
        var isAdmin = User.IsInRole(AppRoles.Admin) || User.IsInRole(AppRoles.SalesStaff) || User.IsInRole(AppRoles.CustomerCareStaff);

        if (isAdmin)
        {
            var resultAdmin = await Sender.Send(new GetAdminProductDetailQuery(id));

            return resultAdmin.IsSuccess ? Ok(resultAdmin.Value) : ToActionResult(resultAdmin);
        }

        var result = await Sender.Send(new GetCustomerProductDetailQuery(id));

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

    [HttpGet("{id}/options")]
    public async Task<IActionResult> GetProductOptions(
        [FromRoute] int id,
        CancellationToken cancellationToken = default
    )
    {
        var result = await Sender.Send(new GetProductOptionsQuery(id), cancellationToken);

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
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProductAsync(
        [FromRoute] int id,
        [FromBody] UpdateProductCommand command,
        CancellationToken cancellationToken = default
    )
    {
        if (id != command.Id)
        {
            var resultTemp = Result.Failure(Error.Validation("Endpoint.InvalidRequest", "Product id in request doesn't match with the id in the route"));
            return ToActionResult(resultTemp);
        }
        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductAsync(
        [FromRoute] int id,
        CancellationToken cancellationToken = default
    )
    {
        var command = new DeleteProductCommand(id);
        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? NoContent() : ToActionResult(result);
    }


    [HttpGet("{id}/ratings")]
    public async Task<IActionResult> GetProductRatings(
        [FromRoute] int id,
        [FromQuery] GetProductRatingListRequest request,
        CancellationToken cancellationToken = default
    )
    {
        if (request.ProductId != id)
        {
            var resultTemp = Result.Failure(Error.Validation("Endpoint.InvalidRequest", "Product id in request doesn't match with the id in the route"));
            return ToActionResult(resultTemp);
        }

        var query = mapper.Map<GetProductRatingListQuery>(request);

        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    [HttpPost("{productId}/variants/{variantId}/ratings")]
    public async Task<IActionResult> CreateProductRating(
        [FromRoute] int id,
        [FromRoute] int variantId,
        [FromBody] CreateProductRatingRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var customerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "0");

        var command = new CreateProductRatingCommand(
            variantId,
            customerId,
            request.Comment,
            request.RatingValue,
            request.ImageUrls
        );

        var result = await Sender.Send(command, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    [HttpPost("{id}/ratings/{ratingId}/like")]
    public async Task<IActionResult> LikeProductRating(
        [FromRoute] int id,
        [FromRoute] int ratingId,
        [FromBody] LikeProductRatingCommand command,
        CancellationToken cancellationToken = default
    )
    {
        if (ratingId != command.RatingId)
        {
            var resultTemp = Result.Failure(Error.Validation("Endpoint.InvalidRequest", "Rating id in request doesn't match with the id in the route"));
            return ToActionResult(resultTemp);
        }
        var result = await Sender.Send(command, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    [HttpPost("{id}/ratings/{ratingId}/report")]
    public async Task<IActionResult> ReportProductRating(
        [FromRoute] int id,
        [FromRoute] int ratingId,
        [FromBody] ReportProductRatingCommand command,
        CancellationToken cancellationToken = default
    )
    {
        if (ratingId != command.RatingId)
        {
            var resultTemp = Result.Failure(Error.Validation("Endpoint.InvalidRequest", "Rating id in request doesn't match with the id in the route"));
            return ToActionResult(resultTemp);
        }
        var result = await Sender.Send(command, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }
    [HttpGet("{id}/related")]
    public async Task<IActionResult> GetRelatedProductsById(
        int id,
        CancellationToken cancellationToken = default
    )
    {
        var query = new GetRelatedProductsByIdQuery(id);
        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    [HttpPost("related/by-image")]
    public async Task<IActionResult> GetRelatedProductsByImage(
        [FromBody] GetRelatedProductsByImageRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var query = mapper.Map<GetRelatedProductsByImageQuery>(request);
        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    /// <summary>
    /// Get products ranked by sentiment analysis from reviews
    /// </summary>
    /// <param name="query">Query parameters for sentiment-based ranking</param>
    /// <returns>Products ordered by sentiment score</returns>
    [HttpGet("sentiment-rankings")]
    public async Task<IActionResult> GetProductsBySentiment(
        [FromQuery] GetProductsBySentimentQuery query,
        CancellationToken cancellationToken = default
    )
    {
        var result = await Sender.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    /// <summary>
    /// Get detailed sentiment analysis for a specific product
    /// </summary>
    /// <param name="productId">Product ID</param>
    /// <returns>Detailed sentiment information for the product</returns>
    [HttpGet("{productId:int}/sentiment")]
    public async Task<IActionResult> GetProductSentiment(
        int productId,
        CancellationToken cancellationToken = default
    )
    {
        var query = new GetProductSentimentQuery(productId);
        var result = await Sender.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }
}