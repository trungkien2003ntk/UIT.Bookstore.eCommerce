namespace KKBookstore.Domain.Shared.Orders;

public static class OrderConsts
{
    private const string DefaultSorting = "CreationTime asc";
    public static string GetDefaultSorting() => DefaultSorting;


    public const int OrderNumberMaxLength = 20;
    public const int CommentMaxLength = 1024;
    public const int DeliveryInstructionMaxLength = 256;
}