namespace KKBookstore.Contracts.Requests;

public class GetOrderListRequest
{
    public string SortBy { get; set; } = "CreationTime";
    public string SortDirection { get; set; } = "desc";
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 12;
    public string? OrderStatuses { get; set; }
    public string? SearchQuery { get; set; }

    // Date Range Filtering
    public DateTimeOffset? FromDate { get; set; }
    public DateTimeOffset? ToDate { get; set; }

    // Customer Filtering
    public int? CustomerId { get; set; }

    // Price Range Filtering
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
}
