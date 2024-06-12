using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Aggregates.ProductAggregate;
using KKBookstore.Domain.Aggregates.ShoppingCartAggregate;
using KKBookstore.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static KKBookstore.Application.Features.ShoppingCarts.UpdateShoppingCartItem.UpdateShoppingCartItemCommand;

namespace KKBookstore.Application.Features.ShoppingCarts.UpdateShoppingCartItem;

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
        public int SkuId { get; init; }
        public int OldSkuId { get; init; }
        public int Quantity { get; init; }
        public int OldQuantity { get; init; }

        //ToEntity
        public ShoppingCartItem ToEntity()
        {
            return new ShoppingCartItem
            {
                Id = Id,
                SkuId = SkuId,
                Quantity = Quantity
            };
        }
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

        var cartItems = await GetCartItems(userId, cancellationToken);
        var createCartResult = ShoppingCart.Create(userId, cartItems);
        if (createCartResult.IsFailure)
        {
            return Result.Failure<UpdateShoppingCartResponse>(ShoppingCartError.Unknown);
        }

        var shoppingCart = createCartResult.Value;
        shoppingCart.SelectItems(request.SelectedItemIds);

        switch (request.ActionType)
        {
            case UpdateCartActionType.SelectForCheckout:
                break;

            case UpdateCartActionType.UpdateQuantity:
                UpdateItemQuantities(shoppingCart, request.UpdateItems);
                break;

            case UpdateCartActionType.UpdateSku:
                UpdateItemSkus(shoppingCart, request.UpdateItems);
                break;

            case UpdateCartActionType.Remove:
                DeleteItems(shoppingCart, request.UpdateItems);
                break;
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
            await _dbContext.Entry(item).Reference(nameof(Sku)).LoadAsync(cancellationToken);
        }

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

        var result = await mappingService.MapToResponse(shoppingCart, discountFromVoucherAmount);

        return result;
    }

    private async Task<List<ShoppingCartItem>> GetCartItems(int userId, CancellationToken cancellationToken)
    {
        return await _dbContext.ShoppingCartItems
            .Where(sci => sci.CustomerId == userId)
            .Include(sci => sci.Sku)
            .ToListAsync(cancellationToken);
    }

    private void UpdateItemQuantities(ShoppingCart shoppingCart, List<UpdateShoppingCartItemBriefDto> listItems)
    {
        var itemIdsToUpdate = listItems.Select(x => x.Id).ToList();

        foreach (var item in shoppingCart.Items.Where(item => itemIdsToUpdate.Contains(item.Id)))
        {
            var updateItem = listItems.First(ui => ui.Id == item.Id);
            // todo: add a check to old quantity
            item.Quantity = updateItem.Quantity;
        }
    }

    private void UpdateItemSkus(ShoppingCart shoppingCart, List<UpdateShoppingCartItemBriefDto> listItems)
    {
        var itemIdsToUpdate = listItems.Select(x => x.Id).ToList();

        foreach (var item in shoppingCart.Items.Where(item => itemIdsToUpdate.Contains(item.Id)))
        {
            var updateItem = listItems.First(ui => ui.Id == item.Id);
            // todo: add a check to old skuId
            item.SkuId = updateItem.SkuId;
        }
    }

    private void DeleteItems(ShoppingCart shoppingCart, List<UpdateShoppingCartItemBriefDto> listItems)
    {
        var itemIdsToUpdate = listItems.Select(x => x.Id).ToList();

        foreach (var itemId in itemIdsToUpdate)
        {
            var itemToRemove = shoppingCart.Items.FirstOrDefault(item => item.Id == itemId);
            if (itemToRemove != null)
            {
                _dbContext.ShoppingCartItems.Remove(itemToRemove);
            }
        }
    }


    
}