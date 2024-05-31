using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Aggregates.ShoppingCartAggregate;
using KKBookstore.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Application.Features.ShoppingCarts;

public class ShoppingCartService(IApplicationDbContext dbContext) : IShoppingCartService
{
    public async Task<Result<ShoppingCart>> GetShoppingCart(int userId, CancellationToken cancellationToken = default)
    {
        // get cart items
        var cartItems = await GetCartItemsAsync(userId, cancellationToken: cancellationToken);

        // create shopping cart
        var createShoppingCartResult = ShoppingCart.Create(userId, cartItems);
        if (createShoppingCartResult.IsFailure)
        {
            return Result.Failure<ShoppingCart>(ShoppingCartError.Unknown);
        }

        return createShoppingCartResult.Value;
    }


    public async Task<List<ShoppingCartItem>> GetCartItemsAsync(int userId, CancellationToken cancellationToken = default)
    {
        var cartItems = await dbContext.ShoppingCartItems
            .Where(sci => sci.CustomerId == userId)
            .Include(sci => sci.Sku)
            .ToListAsync(cancellationToken);

        return cartItems;
    }
}
