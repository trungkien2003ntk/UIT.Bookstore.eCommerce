using KKBookstore.Application.Features.ShoppingCarts.UpdateShoppingCartItem;

namespace KKBookstore.API.Contracts.Requests.ShoppingCart
{
    public class UpdateShoppingCartItemRequest
    {
        public UpdateCartActionType ActionType { get; init; }
        public List<int> SelectedItemIds { get; init; } = [];
        public List<UpdateShoppingCartItemBriefDto> UpdateItems { get; init; } = [];
        
        public UpdateShoppingCartItemCommand ToCommand(int userId)
        {
            return new UpdateShoppingCartItemCommand
            {
                UserId = userId,
                ActionType = ActionType,
                SelectedItemIds = SelectedItemIds,
                UpdateItems = UpdateItems
            };
        }
    }
}
