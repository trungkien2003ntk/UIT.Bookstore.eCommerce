using KKBookstore.Domain.Aggregates.ProductAggregate;
using KKBookstore.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations;

internal class ProductOptionValueConfiguration : IEntityTypeConfiguration<ProductOptionValue>
{
    public void Configure(EntityTypeBuilder<ProductOptionValue> builder)
    {
        builder.ToTable($"{nameof(ProductOptionValue)}s");

        builder.Property(t => t.Id)
            .HasColumnName($"{nameof(ProductOptionValue)}Id");

        builder.Property(t => t.Value)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasOne(t => t.Option)
            .WithMany(o => o.OptionValues)
            .HasForeignKey(t => t.OptionId);

        builder.Property(t => t.ThumbnailImageUrl)
            .HasColumnType("varchar(MAX)")
            .IsRequired();

        builder.Property(t => t.LargeImageUrl)
            .HasColumnType("varchar(MAX)")
            .IsRequired();


        builder.ConfigureAuditing();
    }
}
