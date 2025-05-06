namespace KKBookstore.Features.Branches.Models;

public class BranchSummary
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsDefault { get; set; }
    public bool IsDeleted { get; set; }
    public AddressSummary Address { get; set; } = null!;
    public DateTimeOffset? CreationTime { get; set; }
}

public class AddressSummary
{
    public int ProvinceId { get; set; }
    public string ProvinceName { get; set; } = string.Empty;
    public int DistrictId { get; set; }
    public string DistrictName { get; set; } = string.Empty;
    public string CommuneCode { get; set; } = string.Empty;
    public string CommuneName { get; set; } = string.Empty;
    public string DetailAddress { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string FormattedAddress { get; set; } = string.Empty;
}