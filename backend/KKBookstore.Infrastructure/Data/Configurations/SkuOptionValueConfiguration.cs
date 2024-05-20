using KKBookstore.Domain.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations;

internal class SkuOptionValueConfiguration : IEntityTypeConfiguration<SkuOptionValue>
{
    public void Configure(EntityTypeBuilder<SkuOptionValue> builder)
    {
        builder.Property(t => t.Id)
            .HasColumnName($"{nameof(SkuOptionValue)}Id");

        builder.HasIndex(t => new { t.SkuId, t.OptionId, t.OptionValueId })
            .IsUnique();

        builder.Property(t => t.SkuId)
            .IsRequired();

        builder.Property(t => t.OptionId)
            .IsRequired();

        builder.Property(t => t.OptionValueId)
            .IsRequired();

        builder.HasOne(t => t.Sku)
            .WithMany(t => t.SkuOptionValues)
            .HasForeignKey(t => t.SkuId);

        builder.HasOne(t => t.Option)
            .WithMany()
            .HasForeignKey(t => t.OptionId);

        builder.HasOne(t => t.OptionValue)
            .WithMany()
            .HasForeignKey(t => t.OptionValueId);

        builder.HasOne(t => t.LastEditedByUser)
            .WithMany()
            .HasForeignKey(t => t.LastEditedBy);

        builder.HasOne(t => t.CreatedByUser)
            .WithMany()
            .HasForeignKey(t => t.CreatedBy);
    }
}
