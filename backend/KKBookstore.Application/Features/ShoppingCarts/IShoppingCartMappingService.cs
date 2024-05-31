using KKBookstore.Application.Features.ShoppingCarts.GetShoppingCartItemList;
using KKBookstore.Application.Features.ShoppingCarts.UpdateShoppingCartItem;
using KKBookstore.Domain.Aggregates.ShoppingCartAggregate;
using KKBookstore.Domain.Models;

namespace KKBookstore.Application.Features.ShoppingCarts;

public interface IShoppingCartMappingService
{
    Task<Result<GetShoppingCartResponse>> MapToGetResponse(ShoppingCart shoppingCart);
    Task<Result<ShoppingCartUpdateSummary>> MapToUpdateResponse(ShoppingCart shoppingCart);
}