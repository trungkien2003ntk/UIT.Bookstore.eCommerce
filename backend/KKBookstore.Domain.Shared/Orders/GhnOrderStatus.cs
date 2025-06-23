namespace KKBookstore.Orders;

/// <summary>
/// Represents all possible GHN order statuses based on GHN delivery flow.
/// Maps to the status field in GHN webhook payload.
/// </summary>
public enum GhnOrderStatus
{
    /// <summary>Shipping order has just been created</summary>
    ReadyToPick,

    /// <summary>Shipper is coming to pick up the goods</summary>
    Picking,

    /// <summary>Shipping order has been cancelled</summary>
    Cancel,

    /// <summary>Shipper are interacting with the seller</summary>
    MoneyCollectPicking,

    /// <summary>Shipper is picked the goods</summary>
    Picked,

    /// <summary>The goods has been shipped to GHN sorting hub</summary>
    Storing,

    /// <summary>The goods are being rotated</summary>
    Transporting,

    /// <summary>The goods are being classified (at the warehouse classification)</summary>
    Sorting,

    /// <summary>Shipper is delivering the goods to customer</summary>
    Delivering,

    /// <summary>Shipper is interacting with the buyer</summary>
    MoneyCollectDelivering,

    /// <summary>The goods has been delivered to customer</summary>
    Delivered,

    /// <summary>The goods hasn't been delivered to customer</summary>
    DeliveryFail,

    /// <summary>The goods are pending delivery (can be delivered within 24/48h)</summary>
    WaitingToReturn,

    /// <summary>The goods are waiting to return to seller/merchant after 3 times delivery failed</summary>
    Return,

    /// <summary>The goods are being rotated (return process)</summary>
    ReturnTransporting,

    /// <summary>The goods are being classified (at the warehouse classification) for return</summary>
    ReturnSorting,

    /// <summary>The shipper is returning for seller</summary>
    Returning,

    /// <summary>The returning is failed</summary>
    ReturnFail,

    /// <summary>The goods has been returned to seller/merchant</summary>
    Returned,

    /// <summary>The goods exception handling (cases that go against the process)</summary>
    Exception,

    /// <summary>Damaged goods</summary>
    Damage,

    /// <summary>The goods are lost</summary>
    Lost
}
