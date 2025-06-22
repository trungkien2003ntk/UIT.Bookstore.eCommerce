namespace KKBookstore.Emailing.TemplateModels;

public class AdminBranchSelectionEmailModel : IEmailModel
{
    public string TemplateName => EmailConsts.AdminBranchSelectionEmailTemplateName;

    public string Subject => EmailConsts.AdminBranchSelectionEmailSubject + OrderNumber;

    public string? ReceiverFullName { get; set; }

    public int OrderId { get; }
    public string OrderNumber { get; }
    public string CustomerName { get; }
    public DateTime OrderDate { get; }
    public List<BranchSelectionOption> BranchOptions { get; }
    public decimal TotalOrderValue { get; }    public object TemplateDataModel => new
    {
        RecipientName = ReceiverFullName ?? "Admin",
        OrderId = OrderId,
        OrderNumber = OrderNumber,
        CustomerName = CustomerName,
        OrderDate = OrderDate.ToString("dd/MM/yyyy HH:mm"),
        BranchOptions = BranchOptions.Select(b => new
        {
            BranchId = b.BranchId,
            BranchName = b.BranchName,
            DistanceKm = b.DistanceKm.ToString("F2"),
            TotalItems = b.TotalItems,
            TotalValue = b.TotalValue.ToString("N0")
        }).ToList(),
        TotalOrderValue = TotalOrderValue.ToString("N0")
    };

    public AdminBranchSelectionEmailModel(
        int orderId,
        string orderNumber,
        string customerName,
        DateTime orderDate,
        List<BranchSelectionOption> branchOptions,
        decimal totalOrderValue,
        string? receiverFullName = null)
    {
        OrderId = orderId;
        OrderNumber = orderNumber;
        CustomerName = customerName;
        OrderDate = orderDate;
        BranchOptions = branchOptions;
        TotalOrderValue = totalOrderValue;
        ReceiverFullName = receiverFullName;
    }
}

public class BranchSelectionOption
{
    public int BranchId { get; set; }
    public string BranchName { get; set; } = string.Empty;
    public decimal DistanceKm { get; set; }
    public int TotalItems { get; set; }
    public decimal TotalValue { get; set; }
}
