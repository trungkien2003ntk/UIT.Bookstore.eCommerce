namespace KKBookstore.Domain.Shared.Customers;

public static class ShippingAddressConsts
{
    private const string DefaultSorting = "CreationTime asc";
    public static string GetDefaultSorting() => DefaultSorting;

    public const int ReceiverNameMaxLength = 256;
}