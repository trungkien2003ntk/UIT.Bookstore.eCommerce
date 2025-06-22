namespace KKBookstore.Contracts.Requests;

public class SelectBranchForPackagingRequest
{
    public int BranchId { get; set; }
    public string? Notes { get; set; }
}
