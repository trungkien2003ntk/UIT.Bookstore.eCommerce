using KKBookstore.Models;
using KKBookstore.ShoppingCarts;

namespace KKBookstore.Features.ShoppingCarts.UpdateShoppingCartItem;

public interface IUpdateShoppingCartMappingService
{
    Task<Result<UpdateShoppingCartResponse>> MapToResponse(ShoppingCart shoppingCart, decimal discountFromVoucherAmount);
}