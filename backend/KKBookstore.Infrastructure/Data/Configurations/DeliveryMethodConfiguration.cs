using KKBookstore.Domain.Aggregates.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations;

internal class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
{
    // config this like #AuthorConfiguration
    public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
    {
        builder.ToTable($"{nameof(DeliveryMethod)}s");

        builder.Property(t => t.Id)
            .HasColumnName($"{nameof(DeliveryMethod)}Id");

        builder.HasIndex(t => t.Name)
            .IsUnique();

        builder.Property(t => t.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(t => t.Description)
            .HasMaxLength(500);

        builder.HasOne(t => t.LastEditedByUser)
            .WithMany()
            .HasForeignKey(t => t.LastEditedBy);

        builder.HasOne(t => t.CreatedByUser)
            .WithMany()
            .HasForeignKey(t => t.CreatedBy);
    }
}
