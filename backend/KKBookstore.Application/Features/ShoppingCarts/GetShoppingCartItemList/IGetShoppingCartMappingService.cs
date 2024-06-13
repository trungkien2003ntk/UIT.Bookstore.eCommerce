using KKBookstore.Domain.Aggregates.ShoppingCartAggregate;
using KKBookstore.Domain.Models;

namespace KKBookstore.Application.Features.ShoppingCarts.GetShoppingCartItemList;

public partial interface IGetShoppingCartMappingService
{
    Task<Result<GetShoppingCartResponse>> MapToResponse(ShoppingCart shoppingCart);
}