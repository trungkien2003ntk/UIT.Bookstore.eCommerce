using KKBookstore.API.Abstractions;
using KKBookstore.API.Contracts.Requests;
using KKBookstore.Application.Features.DiscountVouchers.GetAllDiscountVouchers;
using KKBookstore.Application.Features.DiscountVouchers.GetAllDiscountVouchersForCart;
using KKBookstore.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KKBookstore.API.Controllers;


// Note: things need to be done when add discount feature to system:
// 1. Define entities: DiscountVoucher, DiscountApplyToProductType, VoucherUsage - Done
// 2. Define needed endpoints for discount: Get All, Get By Id, Create, Update, Delete
// 3. Handle changes in other places: Order endpoints
//    - Add DiscountVoucherId to checkout request
//    - Add DiscountVoucherId to Order entity



[Route("api/discounts")]
public class DiscountController(ISender sender) : ApiController(sender)
{
    [HttpGet("get-vouchers")]
    public async Task<IActionResult> GetAllDiscountVouchersAsync(
        CancellationToken cancellationToken = default)
    {
        var query = new GetAllDiscountVouchersQuery();

        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }


    [Authorize(Roles = $"{Role.Customer}, {Role.Admin}")]
    [HttpPost("get-vouchers-cart")]
    public async Task<IActionResult> GetAllDiscountVouchersForCartAsync(
        [FromBody] GetAllDiscountVouchersForCartRequest request,
        CancellationToken cancellationToken = default)
    {
        var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!);

        var query = new GetAllDiscountVouchersForCartQuery
        {
            UserId = userId,
            SelectedItemIds = request.SelectedItemIds
        };

        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }
}
