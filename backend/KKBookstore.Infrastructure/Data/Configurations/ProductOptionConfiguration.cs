using KKBookstore.Domain.Aggregates.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations;

public class ProductOptionConfiguration : IEntityTypeConfiguration<ProductOption>
{
    public void Configure(EntityTypeBuilder<ProductOption> builder)
    {
        builder.ToTable($"{nameof(ProductOption)}s");

        builder.Property(t => t.Id)
            .HasColumnName($"{nameof(ProductOption)}Id");

        builder.Property(t => t.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(t => t.IsOptionWithImage)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasOne(t => t.Product)
            .WithMany(t => t.Options)
            .HasForeignKey(t => t.ProductId);

        builder.HasOne(t => t.LastEditedByUser)
            .WithMany()
            .HasForeignKey(t => t.LastEditedBy);

        builder.HasOne(t => t.CreatedByUser)
            .WithMany()
            .HasForeignKey(t => t.CreatedBy);
    }
}
