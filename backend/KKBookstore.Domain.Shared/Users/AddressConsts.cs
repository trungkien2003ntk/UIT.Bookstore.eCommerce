namespace KKBookstore.Domain.Shared.Users;

public static class AddressConsts
{
    private const string DefaultSorting = "CreationTime asc";
    public static string GetDefaultSorting() => DefaultSorting;

    public const int PhoneNumberMaxLength = 13;
    public const int ProvinceNameMaxLength = 1024;
    public const int DistrictNameMaxLength = 1024;
    public const int CommuneNameMaxLength = 1024;
    public const int DetailAddressMaxLength = 2048;
}