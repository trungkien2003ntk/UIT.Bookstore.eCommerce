using KKBookstore.Application.Features.ShoppingCarts.GetShoppingCartItemList;
using KKBookstore.Domain.Models;
using KKBookstore.Domain.ShoppingCarts;

namespace KKBookstore.Features.ShoppingCarts.GetShoppingCartItemList;

public partial interface IGetShoppingCartMappingService
{
    Task<Result<GetShoppingCartResponse>> MapToResponse(ShoppingCart shoppingCart);
}