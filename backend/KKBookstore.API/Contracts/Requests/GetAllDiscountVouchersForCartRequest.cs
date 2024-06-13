namespace KKBookstore.API.Contracts.Requests;

public class GetAllDiscountVouchersForCartRequest
{
    public List<int> SelectedItemIds { get; init; } = [];
}
