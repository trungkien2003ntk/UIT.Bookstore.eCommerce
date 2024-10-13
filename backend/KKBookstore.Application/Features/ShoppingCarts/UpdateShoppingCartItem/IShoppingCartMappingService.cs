using KKBookstore.Domain.Aggregates.ShoppingCartAggregate;
using KKBookstore.Domain.Models;

namespace KKBookstore.Application.Features.ShoppingCarts.UpdateShoppingCartItem;

public interface IUpdateShoppingCartMappingService
{
    Task<Result<UpdateShoppingCartResponse>> MapToResponse(ShoppingCart shoppingCart, decimal discountFromVoucherAmount);
}