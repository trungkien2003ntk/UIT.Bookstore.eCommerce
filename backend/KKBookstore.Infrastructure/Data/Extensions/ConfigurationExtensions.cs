using KKBookstore.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Extensions;

internal static class ConfigurationExtensions
{
    // extensions method for entities that inherit from BaseAuditableEntity (which is also implement ITrackable)
    public static void ConfigureAuditing<T>(this EntityTypeBuilder<T> builder) where T : BaseAuditableEntity
    {
        builder.HasOne(t => t.LastEditedByUser)
            .WithMany()
            .HasForeignKey(t => t.LastEditedByUserId);

        builder.HasOne(t => t.CreatedByUser)
            .WithMany()
            .HasForeignKey(t => t.CreatedByUserId);
    }
}