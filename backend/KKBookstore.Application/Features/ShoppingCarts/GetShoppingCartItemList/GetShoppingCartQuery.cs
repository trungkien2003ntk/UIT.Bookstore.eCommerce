using KKBookstore.Domain.Models;
using MediatR;

namespace KKBookstore.Application.Features.ShoppingCarts.GetShoppingCartItemList;

public record GetShoppingCartQuery(int UserId) : IRequest<Result<GetShoppingCartResponse>>;

public class GetShoppingCartQueryHandler(
    IShoppingCartMappingService mappingService,
    IShoppingCartService cartService
) : IRequestHandler<GetShoppingCartQuery, Result<GetShoppingCartResponse>>
{
    public async Task<Result<GetShoppingCartResponse>> Handle(GetShoppingCartQuery request, CancellationToken cancellationToken)
    {
        var getShoppingCartResult = await cartService.GetShoppingCart(request.UserId, cancellationToken);
        if (getShoppingCartResult.IsFailure)
        {
            return Result.Failure<GetShoppingCartResponse>(getShoppingCartResult.Error);
        }
        var shoppingCart = getShoppingCartResult.Value;

        var result = await mappingService.MapToGetResponse(shoppingCart);

        return result;
    }
}
