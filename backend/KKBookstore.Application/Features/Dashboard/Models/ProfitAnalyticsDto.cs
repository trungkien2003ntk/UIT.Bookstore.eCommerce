namespace KKBookstore.Features.Dashboard.Models;

public class ProfitAnalyticsDto
{
    public decimal TotalProfit { get; set; }
    public decimal PreviousPeriodProfit { get; set; }
    public double GrowthPercentage { get; set; }
    public decimal TotalRevenue { get; set; }
    public decimal TotalCost { get; set; }
    public double ProfitMarginPercentage { get; set; }
    public List<ProfitByPeriodDto> ProfitByPeriod { get; set; } = [];
}

public class ProfitByPeriodDto
{
    public DateTime Date { get; set; }
    public decimal Profit { get; set; }
    public decimal Revenue { get; set; }
    public decimal Cost { get; set; }
    public double ProfitMargin { get; set; }
    public int OrderCount { get; set; }
}
