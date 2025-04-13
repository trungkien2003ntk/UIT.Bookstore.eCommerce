using KKBookstore.Common.Interfaces;
using KKBookstore.Models;
using KKBookstore.ShoppingCarts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.ShoppingCarts.GetShoppingCartItemList;

public record GetShoppingCartQuery(int UserId) : IRequest<Result<GetShoppingCartResponse>>;

public class GetShoppingCartQueryHandler(
    IGetShoppingCartMappingService mappingService,
    IApplicationDbContext dbContext
) : IRequestHandler<GetShoppingCartQuery, Result<GetShoppingCartResponse>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result<GetShoppingCartResponse>> Handle(GetShoppingCartQuery request, CancellationToken cancellationToken)
    {
        var userId = request.UserId;

        var cartItems = await GetCartItems(userId, cancellationToken);
        var getShoppingCartResult = ShoppingCart.Create(userId, cartItems);
        if (getShoppingCartResult.IsFailure)
        {
            return Result.Failure<GetShoppingCartResponse>(getShoppingCartResult.Error);
        }
        var shoppingCart = getShoppingCartResult.Value;

        var result = await mappingService.MapToResponse(shoppingCart);

        return result;
    }

    private async Task<List<ShoppingCartItem>> GetCartItems(int userId, CancellationToken cancellationToken)
    {
        return await _dbContext.ShoppingCartItems
            .Where(sci => sci.CustomerId == userId)
            .Include(sci => sci.ProductVariant)
            .ToListAsync(cancellationToken);
    }
}
