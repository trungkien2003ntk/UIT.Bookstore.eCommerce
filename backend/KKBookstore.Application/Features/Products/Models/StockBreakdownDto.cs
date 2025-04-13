using KKBookstore.Application.Common.Models.ResultDtos;
using KKBookstore.Domain.Shared.Orders;

namespace KKBookstore.Features.Products.Models;

public record StockBreakdownDto : BaseDto
{
    public int BranchId { get; set; }
    public string BranchName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int StockQuantity { get; set; }
    public bool IsActive { get; set; }
    public string Status => IsActive && StockQuantity > 0 ? "In Stock" : "Out of Stock";
    public BranchAddressDto? Address { get; set; }
}

public record BranchAddressDto
{
    public string PhoneNumber { get; set; } = null!;
    public int ProvinceId { get; set; }
    public string ProvinceName { get; set; } = null!;
    public int DistrictId { get; set; }
    public string DistrictName { get; set; } = null!;
    public string CommuneCode { get; set; } = null!;
    public string CommuneName { get; set; } = null!;
    public string DetailAddress { get; set; } = null!;
    public AddressType Type { get; set; }
    
    public string FormattedAddress => $"{DetailAddress}, {CommuneName}, {DistrictName}, {ProvinceName}";
}