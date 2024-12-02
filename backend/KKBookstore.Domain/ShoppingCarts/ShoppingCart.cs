using KKBookstore.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace KKBookstore.Domain.ShoppingCarts;

[NotMapped]
public class ShoppingCart
{
    public int UserId { get; private set; }
    public ICollection<ShoppingCartItem> Items { get; private set; }
    public IEnumerable<ShoppingCartItem> SelectedItems => Items.Where(i => i.IsSelected);
    public decimal TotalUnitPrice => SelectedItems.Sum(i => i.TotalUnitPrice);
    public decimal TotalRecommendedRetailPrice => SelectedItems.Sum(i => i.TotalRecommendedRetailPrice);
    public decimal TotalSavedAmount => SelectedItems.Sum(i => i.TotalSavedAmount);


    private ShoppingCart(int userId, List<ShoppingCartItem> items)
    {
        UserId = userId;
        Items = items;
    }

    public static Result<ShoppingCart> Create(
        int UserId,
        List<ShoppingCartItem> items)
    {
        return new ShoppingCart(UserId, items);
    }

    public Result AddItem(int productVariantId, int newQuantity)
    {
        var existingItem = Items.FirstOrDefault(i => i.ProductVariantId == productVariantId);
        if (existingItem != null)
        {
            existingItem.UpdateQuantity(newQuantity);
            return Result.Success();
        }
        else
        {
            var newShoppingCartItemResult = ShoppingCartItem.Create(UserId, productVariantId, newQuantity);

            if (newShoppingCartItemResult.IsFailure)
            {
                return Result.Failure(newShoppingCartItemResult.Error);
            }

            Items.Add(newShoppingCartItemResult.Value);

            return Result.Success();
        }
    }

    public void RemoveItem(int variantId)
    {
        var existingItem = Items.FirstOrDefault(i => i.ProductVariantId == variantId);
        if (existingItem != null)
        {
            Items.Remove(existingItem);
        }
    }

    public void SelectItems(List<int> ids)
    {
        Items
            .Where(sci => ids.Contains(sci.Id))
            .ToList()
            .ForEach(sci => sci.IsSelected = true);
    }

    public void ClearItems()
    {
        Items.Clear();
    }
}
