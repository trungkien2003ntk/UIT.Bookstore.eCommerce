using KKBookstore.Application.Features.ShoppingCarts.UpdateShoppingCartItem;
using KKBookstore.Domain.Aggregates.ShoppingCartAggregate;
using KKBookstore.Domain.Models;

namespace KKBookstore.Application.Features.ShoppingCarts;

public interface IUpdateShoppingCartMappingService
{
    Task<Result<UpdateShoppingCartResponse>> MapToResponse(ShoppingCart shoppingCart);
}