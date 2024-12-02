namespace KKBookstore.Domain.Shared.Orders;

public static class OrderLineConsts
{
    private const string DefaultSorting = "CreationTime asc";
    public static string GetDefaultSorting() => DefaultSorting;

}