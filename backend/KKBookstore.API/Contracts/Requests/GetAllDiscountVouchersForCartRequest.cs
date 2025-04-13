namespace KKBookstore.Contracts.Requests;

public class GetAllDiscountVouchersForCartRequest
{
    public List<int> SelectedItemIds { get; init; } = [];
}
