namespace KKBookstore.Domain.Shared.Users;

public static class AddressConsts
{
    private const string DefaultSorting = "CreationTime asc";
    public static string GetDefaultSorting() => DefaultSorting;

    public const int PhoneNumberMaxLength = 13;
    public const int ProvinceMaxLength = 125;
    public const int DistrictMaxLength = 125;
    public const int CommuneMaxLength = 125; 
    public const int DetailAddressMaxLength = 1024;
}