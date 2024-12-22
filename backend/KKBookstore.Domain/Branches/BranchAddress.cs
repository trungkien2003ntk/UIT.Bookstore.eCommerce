using KKBookstore.Domain.Shared.Orders;
using KKBookstore.Domain.Users;

namespace KKBookstore.Domain.Branches;

public class BranchAddress : Address
{
    public int BranchId { get; set; }

    public BranchAddress(string phoneNumber, string province, string district, string commune, string detailAddress, bool isDefault, AddressType addressType, int branchId)
        : base(phoneNumber, province, district, commune, detailAddress, isDefault, addressType)
    {
        BranchId = branchId;
    }

    public BranchAddress() : base()
    {
    }
}