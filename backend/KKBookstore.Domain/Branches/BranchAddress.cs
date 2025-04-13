using KKBookstore.Orders;
using KKBookstore.Users;

namespace KKBookstore.Branches;

public class BranchAddress : Address
{
    public int BranchId { get; set; }

    public BranchAddress(
        string phoneNumber,
        int provinceId,
        string province,
        int districtId,
        string district,
        string communeCode,
        string commune,
        string detailAddress,
        bool isDefault,
        AddressType addressType,
        int branchId)
        : base(phoneNumber, provinceId, province, districtId, district, communeCode, commune, detailAddress, isDefault, addressType)
    {
        BranchId = branchId;
    }

    public BranchAddress() : base()
    {
    }
}