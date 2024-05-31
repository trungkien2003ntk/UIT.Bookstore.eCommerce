using KKBookstore.Domain.Aggregates.ShoppingCartAggregate;
using KKBookstore.Domain.Models;

namespace KKBookstore.Application.Features.ShoppingCarts
{
    public interface IShoppingCartService
    {
        Task<List<ShoppingCartItem>> GetCartItemsAsync(int userId, CancellationToken cancellationToken = default);
        Task<Result<ShoppingCart>> GetShoppingCart(int userId, CancellationToken cancellationToken = default);
    }
}