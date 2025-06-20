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
    public decimal TotalOrderValue { get; }

    public object TemplateDataModel => new
    {
        recipient_name = ReceiverFullName ?? "Admin",
        order_id = OrderId,
        order_number = OrderNumber,
        customer_name = CustomerName,
        order_date = OrderDate.ToString("dd/MM/yyyy HH:mm"),
        branch_options = BranchOptions.Select(b => new
        {
            branch_id = b.BranchId,
            branch_name = b.BranchName,
            distance_km = b.DistanceKm.ToString("F2"),
            total_items = b.TotalItems,
            total_value = b.TotalValue.ToString("N0")
        }).ToList(),
        total_order_value = TotalOrderValue.ToString("N0")
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
