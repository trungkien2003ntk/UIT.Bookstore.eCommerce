using KKBookstore.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace KKBookstore.Domain.Aggregates.ShoppingCartAggregate;

[NotMapped]
public class ShoppingCart
{
    public int UserId { get; private set; }
    public ICollection<ShoppingCartItem> Items { get; private set; }
    public IEnumerable<ShoppingCartItem> SelectedItems => Items.Where(i => i.IsSelected);
    public decimal TotalUnitPrice => SelectedItems.Sum(i => i.GetTotalUnitPrice());
    public decimal TotalRecommendedRetailPrice => SelectedItems.Sum(i => i.GetTotalRecommendedRetailPrice());
    public decimal TotalSavedAmount => SelectedItems.Sum(i => i.GetTotalSavedAmount());


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

    public Result AddItem(int skuId, int quantity)
    {
        var existingItem = Items.FirstOrDefault(i => i.SkuId == skuId);
        if (existingItem != null)
        {
            existingItem.AddQuantity(quantity);
            return Result.Success();
        }
        else
        {
            var newShoppingCartItemResult = ShoppingCartItem.Create(UserId, skuId, quantity);

            if (newShoppingCartItemResult.IsFailure)
            {
                return Result.Failure(newShoppingCartItemResult.Error);
            }

            Items.Add(newShoppingCartItemResult.Value);

            return Result.Success();
        }
    }

    public void RemoveItem(int skuId)
    {
        var existingItem = Items.FirstOrDefault(i => i.SkuId == skuId);
        if (existingItem != null)
        {
            Items.Remove(existingItem);
        }
    }

    public void ClearItems()
    {
        Items.Clear();
    }
}
