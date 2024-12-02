using KKBookstore.Domain.Orders;
using KKBookstore.Domain.Shared.Orders;
using KKBookstore.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations.Orders;

internal class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
{
    // config this like #AuthorConfiguration
    public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
    {
        builder.ToTable($"DeliveryMethods");
        builder.ConfigureAuditing();

        builder.Property(t => t.Name).HasColumnName(nameof(DeliveryMethod.Name)).HasMaxLength(DeliveryMethodConsts.NameMaxLength).IsRequired();
        builder.Property(t => t.Description).HasColumnName(nameof(DeliveryMethod.Description)).HasMaxLength(DeliveryMethodConsts.DescriptionMaxLength);

        builder.HasIndex(t => t.Name)
            .IsUnique();
    }
}
