using KKBookstore.Domain.Interfaces;
using KKBookstore.Domain.Models;
using KKBookstore.Domain.Shared.Orders;
using System.ComponentModel.DataAnnotations.Schema;

namespace KKBookstore.Domain.Orders;

public class PaymentMethod : BaseFullAuditedEntity
{
    public PaymentMethod()
    {
        Name = string.Empty;
        Description = null;
    }

    public PaymentMethod(string name, string? description = null)
    {
        Name = name;
        Description = description;
    }

    public string Name { get; set; }
    public string? Description { get; set; }

    [NotMapped]
    public PaymentMethodType Type => (PaymentMethodType)Id;
}
