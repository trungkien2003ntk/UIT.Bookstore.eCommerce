using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Aggregates.ShoppingCartAggregate;
using KKBookstore.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Application.Features.ShoppingCarts.AddShoppingCartItem;

public record AddShoppingCartItemCommand(int CustomerId, int SkuId, int Quantity) : IRequest<Result<AddShoppingCartItemResponse>>;

public class AddShoppingCartItemCommandHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<AddShoppingCartItemCommand, Result<AddShoppingCartItemResponse>>
{
    public async Task<Result<AddShoppingCartItemResponse>> Handle(AddShoppingCartItemCommand request, CancellationToken cancellationToken)
    {
        // check valid sku
        var sku = await dbContext.Skus.FindAsync([request.SkuId], cancellationToken: cancellationToken);

        if (sku is null)
        {
            return Result.Failure<AddShoppingCartItemResponse>(ShoppingCartError.InvalidSkuToAdd);
        }

        var existingShoppingCartItem = await dbContext.ShoppingCartItems
            .Where(sci => sci.CustomerId == request.CustomerId && sci.SkuId == request.SkuId)
            .FirstOrDefaultAsync(cancellationToken);

        if (existingShoppingCartItem is not null)
        {
            existingShoppingCartItem.Quantity += request.Quantity;
            await dbContext.SaveChangesAsync(cancellationToken);
            return Result.Success(new AddShoppingCartItemResponse(existingShoppingCartItem));
        }


        var newShoppingCartItemResult = ShoppingCartItem.Create(request.CustomerId, request.SkuId, request.Quantity);

        if (newShoppingCartItemResult.IsFailure)
        {
            return Result.Failure<AddShoppingCartItemResponse>(newShoppingCartItemResult.Error);
        }

        var newShoppingCartItem = newShoppingCartItemResult.Value;
        newShoppingCartItem.Sku = sku;

        await dbContext.ShoppingCartItems.AddAsync(newShoppingCartItem, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success(new AddShoppingCartItemResponse(newShoppingCartItem));
    }
}