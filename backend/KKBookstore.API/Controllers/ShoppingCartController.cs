using KKBookstore.API.Abstractions;
using KKBookstore.API.Contracts.Requests.ShoppingCart;

using KKBookstore.Application.Features.ShoppingCarts.AddShoppingCartItem;
using KKBookstore.Application.Features.ShoppingCarts.GetShoppingCartItemList;
using KKBookstore.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KKBookstore.API.Controllers;

[Authorize(Roles = $"{Role.Customer}")]
[Route("api/[controller]")]
public class ShoppingCartController(
    ISender sender    
) : ApiController(sender)
{
    [HttpGet]
    public async Task<IActionResult> GetShoppingCartItemsAsync(
        CancellationToken cancellationToken = default
    )
    {
        var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!);

        var query = new GetShoppingCartQuery(userId);
        var result = await Sender.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }


    [HttpPost("add")]
    public async Task<IActionResult> AddShoppingCartItemAsync(
        [FromBody] AddShoppingCartItemRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!);
        var command = new AddShoppingCartItemCommand(userId, request.SkuId, request.Quantity);

        var result = await Sender.Send(command, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    
    [HttpPost("update")]
    public async Task<IActionResult> UpdateShoppingCartAsync(
        [FromBody] UpdateShoppingCartItemRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!);
        var command = request.ToCommand(userId);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }
     

    [HttpDelete]
    public IActionResult RemoveFromShoppingCart()
    {
        return Ok();
    }
}
