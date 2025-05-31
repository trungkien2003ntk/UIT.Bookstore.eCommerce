using KKBookstore.Common.Models.ResultDtos;
using KKBookstore.Customers;

namespace KKBookstore.Features.CustomerTypes.Models;

public record CustomerTypeSummary : BaseFullAuditedDto
{
    public string Name { get; set; } = string.Empty;
    public CustomerTier Tier { get; set; }
    public decimal MinSpending { get; set; }
}
