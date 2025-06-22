namespace KKBookstore.Contracts.Requests;

public class ConfirmPackagingCompleteRequest
{
    public int BranchId { get; set; }
    public string? Notes { get; set; }
}
