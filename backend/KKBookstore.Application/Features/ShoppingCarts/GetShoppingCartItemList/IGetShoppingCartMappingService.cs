using KKBookstore.Models;
using KKBookstore.ShoppingCarts;

namespace KKBookstore.Features.ShoppingCarts.GetShoppingCartItemList;

public partial interface IGetShoppingCartMappingService
{
    Task<Result<GetShoppingCartResponse>> MapToResponse(ShoppingCart shoppingCart);
}