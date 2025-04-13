namespace KKBookstore.Customers;

public static class ShippingAddressConsts
{
    private const string DefaultSorting = "CreationTime asc";
    public static string GetDefaultSorting() => DefaultSorting;

    public const int ReceiverNameMaxLength = 256;
}