using KKBookstore.Orders;

namespace KKBookstore.Features.Branches.Models;

public class BranchDetail
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsDefault { get; set; }
    public bool IsDeleted { get; set; }
    public AddressDetail Address { get; set; } = null!;
    public DateTimeOffset? CreationTime { get; set; }
    public int? CreatorId { get; set; }
    public DateTimeOffset? LastModificationTime { get; set; }
    public int? LastModifierId { get; set; }
}

public class AddressDetail
{
    public int Id { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public int ProvinceId { get; set; }
    public string ProvinceName { get; set; } = string.Empty;
    public int DistrictId { get; set; }
    public string DistrictName { get; set; } = string.Empty;
    public string CommuneCode { get; set; } = string.Empty;
    public string CommuneName { get; set; } = string.Empty;
    public string DetailAddress { get; set; } = string.Empty;
    public AddressType AddressType { get; set; }
    public string FormattedAddress { get; set; } = string.Empty;
}