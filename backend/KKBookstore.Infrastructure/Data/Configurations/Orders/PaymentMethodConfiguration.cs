using KKBookstore.Data.Extensions;
using KKBookstore.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Data.Configurations.Orders;

internal class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
{
    public void Configure(EntityTypeBuilder<PaymentMethod> builder)
    {
        builder.ToTable("PaymentMethods");
        builder.ConfigureAuditing();

        builder.Property(t => t.Name).HasColumnName(nameof(PaymentMethod.Name)).HasMaxLength(PaymentMethodConsts.NameMaxLength).IsRequired();
        builder.Property(t => t.Description).HasColumnName(nameof(PaymentMethod.Description)).HasMaxLength(PaymentMethodConsts.DescriptionMaxLength);
        builder.HasIndex(t => t.Name).IsUnique();
    }
}
