namespace KKBookstore.Features.Dashboard.Models;

public class DashboardSummaryDto
{
    public int TotalOrders { get; set; }
    public decimal TotalOrdersChangePercent { get; set; }

    public int TotalNewUsers { get; set; }
    public decimal TotalNewUsersChangePercent { get; set; }

    public int TotalStockAdjustmentOrders { get; set; }
    public decimal TotalStockAdjustmentOrdersChangePercent { get; set; }

    public decimal TotalRevenue { get; set; }
    public decimal TotalRevenueChangePercent { get; set; }

    public decimal AverageOrderValue { get; set; }
    public decimal AverageOrderValueChangePercent { get; set; }

    public int TotalProductsSold { get; set; }
    public decimal TotalProductsSoldChangePercent { get; set; }

    public List<SalesByProductTypeDto> SalesByProductTypes { get; set; } = [];
    public List<TopProductDto> TopProducts { get; set; } = [];
}

public class SalesByProductTypeDto
{
    public int ProductTypeId { get; set; }
    public string ProductTypeName { get; set; } = string.Empty;
    public int TotalQuantitySold { get; set; }
    public decimal TotalRevenue { get; set; }
    public decimal RevenueChangePercent { get; set; }
}

public class TopProductDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string ProductTypeName { get; set; } = string.Empty;
    public int TotalQuantitySold { get; set; }
    public decimal TotalRevenue { get; set; }
    public string? ProductImageUrl { get; set; }
    public decimal QuantityChangePercent { get; set; }
}

public class RevenueAnalyticsDto
{
    public decimal TotalRevenue { get; set; }
    public decimal PreviousPeriodRevenue { get; set; }
    public double GrowthPercentage { get; set; }
    public List<RevenueByPeriodDto> RevenueByPeriod { get; set; } = [];
}

public class RevenueByPeriodDto
{
    public DateTime Date { get; set; }
    public decimal Revenue { get; set; }
    public int OrderCount { get; set; }
}

public class CustomerAnalyticsDto
{
    public int TotalCustomers { get; set; }
    public int NewCustomers { get; set; }
    public int ActiveCustomers { get; set; }
    public double CustomerRetentionRate { get; set; }
    public decimal AverageCustomerValue { get; set; }
    public List<CustomerGrowthDto> CustomerGrowth { get; set; } = [];
}

public class CustomerGrowthDto
{
    public DateTime Date { get; set; }
    public int NewCustomers { get; set; }
    public int TotalCustomers { get; set; }
}

public class InventoryAnalyticsDto
{
    public int TotalProducts { get; set; }
    public int LowStockProducts { get; set; }
    public int OutOfStockProducts { get; set; }
    public int TotalStockTransactions { get; set; }
    public List<LowStockProductDto> LowStockProductsList { get; set; } = [];
}

public class LowStockProductDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int CurrentStock { get; set; }
    public int MinimumStock { get; set; }
    public string ProductTypeName { get; set; } = string.Empty;
}

public class OrderAnalyticsDto
{
    public int TotalOrders { get; set; }
    public int PendingOrders { get; set; }
    public int ProcessingOrders { get; set; }
    public int ShippedOrders { get; set; }
    public int DeliveredOrders { get; set; }
    public int CancelledOrders { get; set; }
    public double OrderFulfillmentRate { get; set; }
    public double AverageProcessingTime { get; set; }
    public List<OrderStatusDistributionDto> OrderStatusDistribution { get; set; } = [];
}

public class OrderStatusDistributionDto
{
    public string Status { get; set; } = string.Empty;
    public int Count { get; set; }
    public double Percentage { get; set; }
}
