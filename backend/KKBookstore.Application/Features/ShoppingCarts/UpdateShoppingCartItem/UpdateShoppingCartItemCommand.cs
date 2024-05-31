using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Aggregates.ProductAggregate;
using KKBookstore.Domain.Aggregates.ShoppingCartAggregate;
using KKBookstore.Domain.Models;
using MediatR;

namespace KKBookstore.Application.Features.ShoppingCarts.UpdateShoppingCartItem;

public record UpdateShoppingCartItemCommand : IRequest<Result<ShoppingCartUpdateSummary>>
{
    public int UserId { get; init; }
    public UpdateCartActionType ActionType { get; init; }
    public List<int> SelectedItemIds { get; init; } = [];
    public List<UpdateShoppingCartItemBriefDto> UpdateItems { get; init; } = [];
}

public class UpdateShoppingCartItemCommandHandler(
    IApplicationDbContext dbContext,
    IShoppingCartMappingService mappingService,
    IShoppingCartService cartService
) : IRequestHandler<UpdateShoppingCartItemCommand, Result<ShoppingCartUpdateSummary>>
{
    public async Task<Result<ShoppingCartUpdateSummary>> Handle(UpdateShoppingCartItemCommand request, CancellationToken cancellationToken)
    {
        var getShoppingCartResult = await cartService.GetShoppingCart(request.UserId, cancellationToken);
        if (getShoppingCartResult.IsFailure)
        {
            return Result.Failure<ShoppingCartUpdateSummary>(getShoppingCartResult.Error);
        }
        var shoppingCart = getShoppingCartResult.Value;

        shoppingCart.Items
                    .Where(sci => request.SelectedItemIds.Contains(sci.Id))
                    .ToList()
                    .ForEach(sci => sci.IsSelected = true);

        var itemIdsToUpdate = request.UpdateItems.Select(x => x.Id).ToList();

        switch (request.ActionType)
        {
            case UpdateCartActionType.SelectForCheckout:
                break;

            case UpdateCartActionType.UpdateQuantity:
                foreach (var item in shoppingCart.Items.Where(item => itemIdsToUpdate.Contains(item.Id)))
                {
                    var updateItem = request.UpdateItems.First(ui => ui.Id == item.Id);
                    // todo: add a check to old quantity
                    item.Quantity = updateItem.Quantity;
                }
                break;

            case UpdateCartActionType.UpdateSku:
                foreach (var item in shoppingCart.Items.Where(item => itemIdsToUpdate.Contains(item.Id)))
                {
                    var updateItem = request.UpdateItems.First(ui => ui.Id == item.Id);
                    // todo: add a check to old skuId
                    item.SkuId = updateItem.SkuId;
                }
                break;

            case UpdateCartActionType.Remove:
                foreach (var itemId in itemIdsToUpdate)
                {
                    var itemToRemove = shoppingCart.Items.FirstOrDefault(item => item.Id == itemId);
                    if (itemToRemove != null)
                    {
                        dbContext.ShoppingCartItems.Remove(itemToRemove);
                    }
                }

                break;
        }

        try
        {   
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch
        {
            return Result.Failure<ShoppingCartUpdateSummary>(ShoppingCartError.Unknown);
        }

        // load the sku navigation property for the updated items, this is because the sku navigation property for the items that changed skuId
        // will be null after saving changes

        foreach (var item in shoppingCart.Items.Where(item => itemIdsToUpdate.Contains(item.Id)))
        {
            
            await dbContext.Entry(item).Reference(nameof(Sku)).LoadAsync(cancellationToken);
        }
        
        var result = await mappingService.MapToUpdateResponse(shoppingCart);

        return result;
    }
}