namespace KKBookstore.API.Contracts.Requests;

public class GetAllDiscountVouchersRequest
{
    public List<int> SelectedItemIds { get; init; } = [];
}
