namespace KKBookstore.Contracts.Requests.Dashboard;

public class GetDashboardSummaryRequest
{
    public DateTimeOffset? FromDate { get; set; }
    public DateTimeOffset? ToDate { get; set; }
    public string Period { get; set; } = "month"; // week, month, year
    public int? BranchId { get; set; }
    public List<int> ProductTypeIds { get; set; } = [];
    public int TopProductsLimit { get; set; } = 5;
}

public class GetRevenueAnalyticsRequest
{
    public DateTimeOffset? FromDate { get; set; }
    public DateTimeOffset? ToDate { get; set; }
    public string Period { get; set; } = "month"; // day, week, month, year
    public string GroupBy { get; set; } = "day"; // day, week, month
    public int? BranchId { get; set; }
    public List<int> ProductTypeIds { get; set; } = [];
}

public class GetCustomerAnalyticsRequest
{
    public DateTimeOffset? FromDate { get; set; }
    public DateTimeOffset? ToDate { get; set; }
    public string Period { get; set; } = "month"; // week, month, year
    public string GroupBy { get; set; } = "day"; // day, week, month
    public int? BranchId { get; set; }
}

public class GetInventoryAnalyticsRequest
{
    public DateTimeOffset? FromDate { get; set; }
    public DateTimeOffset? ToDate { get; set; }
    public string Period { get; set; } = "month"; // week, month, year
    public int? BranchId { get; set; }
    public List<int> ProductTypeIds { get; set; } = [];
    public int LowStockThreshold { get; set; } = 10;
    public int LowStockProductsLimit { get; set; } = 20;
}

public class GetOrderAnalyticsRequest
{
    public DateTimeOffset? FromDate { get; set; }
    public DateTimeOffset? ToDate { get; set; }
    public string Period { get; set; } = "month"; // week, month, year
    public int? BranchId { get; set; }
    public List<string> OrderStatuses { get; set; } = [];
}
