using KKBookstore.Common.Interfaces;
using KKBookstore.Models;
using KKBookstore.ShoppingCarts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static KKBookstore.Features.ShoppingCarts.UpdateShoppingCartItem.UpdateShoppingCartItemCommand;

namespace KKBookstore.Features.ShoppingCarts.UpdateShoppingCartItem;

public record UpdateShoppingCartItemCommand : IRequest<Result<UpdateShoppingCartResponse>>
{
    public int UserId { get; init; }
    public UpdateCartActionType ActionType { get; init; }
    public List<int> SelectedItemIds { get; init; } = [];
    public List<UpdateShoppingCartItemBriefDto> UpdateItems { get; init; } = [];
    public int? OrderDiscountVoucherId { get; init; }
    public int? ShippingDiscountVoucherId { get; init; }

    public sealed record UpdateShoppingCartItemBriefDto
    {
        public int Id { get; init; }
        public int ProductVariantId { get; init; }
        public int OldProductVariantId { get; init; }
        public int Quantity { get; init; }
        public int OldQuantity { get; init; }
    }
}

public class UpdateShoppingCartItemCommandHandler(
    IApplicationDbContext dbContext,
    IUpdateShoppingCartMappingService mappingService
) : IRequestHandler<UpdateShoppingCartItemCommand, Result<UpdateShoppingCartResponse>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result<UpdateShoppingCartResponse>> Handle(UpdateShoppingCartItemCommand request, CancellationToken cancellationToken)
    {
        var userId = request.UserId;
        var orderDiscountVoucherId = request.OrderDiscountVoucherId;
        var shippingDiscountVoucherId = request.ShippingDiscountVoucherId;
        var updateActionType = request.ActionType;

        var cartItems = await GetCartItems(userId, cancellationToken);
        var createCartResult = ShoppingCart.Create(userId, cartItems);
        if (createCartResult.IsFailure)
        {
            return Result.Failure<UpdateShoppingCartResponse>(ShoppingCartError.Unknown);
        }

        var shoppingCart = createCartResult.Value;
        shoppingCart.SelectItems(request.SelectedItemIds);

        Result<List<ShoppingCartItem>> updateResult;
        switch (updateActionType)
        {
            case UpdateCartActionType.UpdateQuantity:
                updateResult = UpdateItemQuantities(shoppingCart, request.UpdateItems);
                break;

            case UpdateCartActionType.UpdateProductVariant:
                updateResult = UpdateItemProductVariants(shoppingCart, request.UpdateItems);
                break;

            case UpdateCartActionType.Remove:
                updateResult = DeleteItems(shoppingCart, request.UpdateItems);
                break;

            case UpdateCartActionType.SelectForCheckout:
            default:
                updateResult = Result.Success<List<ShoppingCartItem>>([]);
                break;
        }

        if (updateResult.IsFailure)
        {
            return Result.Failure<UpdateShoppingCartResponse>(updateResult.Error);
        }

        try
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch
        {
            return Result.Failure<UpdateShoppingCartResponse>(ShoppingCartError.Unknown);
        }

        // load the sku navigation property for the updated items, this is because the sku navigation property for the items that changed skuId
        // will be null after saving changes

        var itemIdsToUpdate = request.UpdateItems.Select(x => x.Id).ToList();
        foreach (var item in shoppingCart.Items.Where(item => itemIdsToUpdate.Contains(item.Id)))
        {
            await _dbContext.Entry(item)
                .Reference(i => i.ProductVariant)
                .Query()
                .Include(pv => pv.Inventories)
                .LoadAsync(cancellationToken);
        }

        // Calculate discount from voucher
        var orderDiscountVoucher = await _dbContext.DiscountVouchers
            .FirstOrDefaultAsync(dv => dv.Id == orderDiscountVoucherId, cancellationToken);

        var shippingDiscountVoucher = await _dbContext.DiscountVouchers
            .FirstOrDefaultAsync(dv => dv.Id == shippingDiscountVoucherId, cancellationToken);

        var discountFromVoucherAmount = 0m;
        if (orderDiscountVoucher != null)
        {
            discountFromVoucherAmount += orderDiscountVoucher.GetDiscountValue(shoppingCart.TotalUnitPrice);
        }

        if (shippingDiscountVoucher != null)
        {
            discountFromVoucherAmount += shippingDiscountVoucher.GetDiscountValue(shoppingCart.TotalUnitPrice);
        }

        List<ShoppingCartItem> updatedAndSelectedItems = shoppingCart.Items
            .Where(item => item.IsSelected || itemIdsToUpdate.Contains(item.Id))
            .ToList();

        var createCartResponseResult = ShoppingCart.Create(userId, updatedAndSelectedItems);

        if (createCartResponseResult.IsFailure)
        {
            return Result.Failure<UpdateShoppingCartResponse>(ShoppingCartError.Unknown);
        }

        // Re-select the items that were originally selected since IsSelected is not persisted
        createCartResponseResult.Value.SelectItems(request.SelectedItemIds);

        var result = await mappingService.MapToResponse(createCartResponseResult.Value, discountFromVoucherAmount);

        return result;
    }

    private async Task<List<ShoppingCartItem>> GetCartItems(int userId, CancellationToken cancellationToken)
    {
        return await _dbContext.ShoppingCartItems
            .Where(sci => sci.CustomerId == userId)
            .Include(sci => sci.ProductVariant)
                .ThenInclude(pv => pv.Inventories)
            .ToListAsync(cancellationToken);
    }

    private Result<List<ShoppingCartItem>> UpdateItemQuantities(ShoppingCart shoppingCart, List<UpdateShoppingCartItemBriefDto> listItems)
    {
        var updatedItems = new List<ShoppingCartItem>();

        var itemIdsToUpdate = listItems.Select(x => x.Id).ToList();

        foreach (var item in shoppingCart.Items.Where(item => itemIdsToUpdate.Contains(item.Id)))
        {
            var updateItem = listItems.First(ui => ui.Id == item.Id);
            // todo: add a check to old quantity
            if (updateItem.Quantity > item.ProductVariant.AvailableQuantity)
            {
                return Result.Failure<List<ShoppingCartItem>>(ShoppingCartError.NotEnoughStock);
            }

            item.Quantity = updateItem.Quantity;

            updatedItems.Add(item);
        }

        return updatedItems;
    }

    private Result<List<ShoppingCartItem>> UpdateItemProductVariants(ShoppingCart shoppingCart, List<UpdateShoppingCartItemBriefDto> listItems)
    {
        List<ShoppingCartItem> updatedItems = [];
        var itemIdsToUpdate = listItems.Select(x => x.Id).ToList();

        foreach (var item in shoppingCart.Items.Where(item => itemIdsToUpdate.Contains(item.Id)))
        {
            var updateItem = listItems.First(ui => ui.Id == item.Id);
            // todo: add a check to old skuId
            item.ProductVariantId = updateItem.ProductVariantId;

            updatedItems.Add(item);
        }

        return updatedItems;
    }

    private Result<List<ShoppingCartItem>> DeleteItems(ShoppingCart shoppingCart, List<UpdateShoppingCartItemBriefDto> listItems)
    {
        List<ShoppingCartItem> updatedItems = [];
        var itemIdsToUpdate = listItems.Select(x => x.Id).ToList();

        foreach (var itemId in itemIdsToUpdate)
        {
            var itemToRemove = shoppingCart.Items.FirstOrDefault(item => item.Id == itemId);
            if (itemToRemove != null)
            {
                _dbContext.ShoppingCartItems.Remove(itemToRemove);
                updatedItems.Add(itemToRemove);
            }
        }

        return updatedItems;
    }
}