
using System;

namespace KKBookstore.Data.Entities;


public class OrderLine : BaseEntity, ITrackable
{
    public int OrderLineID { get; set; }
    public int OrderID { get; set; }
    public int SkuID { get; set; }
    public string Description { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public DateTimeOffset PickingCompletedWhen { get; set; }
    public int? DiscountID { get; set; }
    public int LastEditedBy { get; set; }
    public DateTimeOffset LastEditedWhen { get; set; }

    // navigation property
    public User LastEditedByUser { get; set; }
    public Order Order { get; set; }
    public Sku Sku { get; set; }
    public DiscountVoucher DiscountVoucher { get; set; }

}
