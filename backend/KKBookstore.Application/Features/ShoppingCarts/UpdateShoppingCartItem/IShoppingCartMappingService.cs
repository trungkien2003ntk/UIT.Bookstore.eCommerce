using KKBookstore.Domain.Models;
using KKBookstore.Domain.ShoppingCarts;

namespace KKBookstore.Application.Features.ShoppingCarts.UpdateShoppingCartItem;

public interface IUpdateShoppingCartMappingService
{
    Task<Result<UpdateShoppingCartResponse>> MapToResponse(ShoppingCart shoppingCart, decimal discountFromVoucherAmount);
}