namespace KKBookstore.Model.Constants
{
    public enum UserStatus
    {
        Active,
        Inactive,
        Comfirm,
        UnComfirm
    }

    public enum OrderStatus
    {
        Pending,
        PaymentProcessing,
        PaymentConfirmed,
        AwaitingFulfillment,
        Packed,
        Shipped,
        OutForDelivery,
        Delivered,
        Cancelled
    }

    public enum StockStatus
    {
        InStock,
        LowStock,
        OutOfStock
    }

    public enum TypeLogin
    {
        Google,
        Facebook,
        Email
    }

    


}
