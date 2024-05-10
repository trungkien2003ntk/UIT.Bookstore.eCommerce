using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations;

public class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
{
    // config this like #AuthorConfiguration
    public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
    {
        builder.Property(t => t.Id)
            .HasColumnName("DeliveryMethodId");

        builder.HasIndex(t => t.Name)
            .IsUnique();

        builder.Property(t => t.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(t => t.Description)
            .HasMaxLength(500);
    }
}
