using KKBookstore.Data.Extensions;
using KKBookstore.Products.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Data.Configurations.Products;

internal class RatingReportConfiguration : IEntityTypeConfiguration<RatingReport>
{
    public void Configure(EntityTypeBuilder<RatingReport> builder)
    {
        builder.ToTable("RatingReports");
        builder.ConfigureAuditing(); builder.Property(t => t.RatingId).HasColumnName(nameof(RatingReport.RatingId)).IsRequired();
        builder.Property(t => t.CustomerId).HasColumnName(nameof(RatingReport.CustomerId));
        builder.Property(t => t.Reason).HasColumnName(nameof(RatingReport.Reason)).IsRequired();
        builder.Property(t => t.DetailedReason).HasColumnName(nameof(RatingReport.DetailedReason)).IsRequired(false);

        builder.HasOne(t => t.Rating).WithMany(t => t.Reports).HasForeignKey(t => t.RatingId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(t => t.Customer).WithMany().HasForeignKey(t => t.CustomerId).OnDelete(DeleteBehavior.SetNull);
    }
}
