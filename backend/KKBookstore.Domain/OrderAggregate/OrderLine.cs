
using System;
using KKBookstore.Domain.Common.Interfaces;
using KKBookstore.Domain.DiscountAggregate;
using KKBookstore.Domain.ProductAggregate;
using KKBookstore.Domain.Users;

namespace KKBookstore.Domain.OrderAggregate;


public class OrderLine : BaseEntity, ITrackable
{
    public int OrderLineId { get; set; }
    public int OrderId { get; set; }
    public int SkuId { get; set; }
    public string Description { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public DateTimeOffset PickingCompletedWhen { get; set; }
    public int? DiscountVoucherId { get; set; }
    public int LastEditedBy { get; set; }
    public DateTimeOffset LastEditedWhen { get; set; }

    // navigation property
    public Order Order { get; set; }
    public Sku Sku { get; set; }
    public DiscountVoucher DiscountVoucher { get; set; }
    public User LastEditedByUser { get; set; }

}
