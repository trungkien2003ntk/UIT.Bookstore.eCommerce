using KKBookstore.Domain.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations;

internal class ProductOptionValueConfiguration : IEntityTypeConfiguration<ProductOptionValue>
{
    public void Configure(EntityTypeBuilder<ProductOptionValue> builder)
    {
        builder.Property(t => t.Id)
            .HasColumnName($"{nameof(ProductOptionValue)}Id");

        builder.Property(t => t.Value)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasOne(t => t.Option)
            .WithMany(o => o.OptionValues)
            .HasForeignKey(t => t.OptionId);

        builder.HasOne(t => t.LastEditedByUser)
            .WithMany()
            .HasForeignKey(t => t.LastEditedBy);

        builder.HasOne(t => t.CreatedByUser)
            .WithMany()
            .HasForeignKey(t => t.CreatedBy);
    }
}
