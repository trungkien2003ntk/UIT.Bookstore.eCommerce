using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Models;
using KKBookstore.Domain.ShoppingCarts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Application.Features.ShoppingCarts.AddShoppingCartItem;

public record AddShoppingCartItemCommand(int CustomerId, int ProductVariantId, int Quantity) : IRequest<Result<AddShoppingCartItemResponse>>;

public class AddShoppingCartItemCommandHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<AddShoppingCartItemCommand, Result<AddShoppingCartItemResponse>>
{
    public async Task<Result<AddShoppingCartItemResponse>> Handle(AddShoppingCartItemCommand request, CancellationToken cancellationToken)
    {
        // check valid sku
        var productVariant = await dbContext.ProductVariants.FindAsync([request.ProductVariantId], cancellationToken: cancellationToken);

        if (productVariant is null)
        {
            return Result.Failure<AddShoppingCartItemResponse>(ShoppingCartError.InvalidProductVariantToAdd);
        }

        var existingShoppingCartItem = await dbContext.ShoppingCartItems
            .Where(sci => sci.CustomerId == request.CustomerId && sci.ProductVariantId == request.ProductVariantId)
            .FirstOrDefaultAsync(cancellationToken);

        if (existingShoppingCartItem is not null)
        {
            existingShoppingCartItem.Quantity += request.Quantity;
            await dbContext.SaveChangesAsync(cancellationToken);
            return Result.Success(new AddShoppingCartItemResponse(existingShoppingCartItem));
        }


        var newShoppingCartItemResult = ShoppingCartItem.Create(request.CustomerId, request.ProductVariantId, request.Quantity);

        if (newShoppingCartItemResult.IsFailure)
        {
            return Result.Failure<AddShoppingCartItemResponse>(newShoppingCartItemResult.Error);
        }

        var newShoppingCartItem = newShoppingCartItemResult.Value;
        newShoppingCartItem.ProductVariant = productVariant;

        await dbContext.ShoppingCartItems.AddAsync(newShoppingCartItem, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success(new AddShoppingCartItemResponse(newShoppingCartItem));
    }
}