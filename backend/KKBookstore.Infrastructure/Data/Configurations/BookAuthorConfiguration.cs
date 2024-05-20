﻿using KKBookstore.Domain.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations;

internal class BookAuthorConfiguration : IEntityTypeConfiguration<BookAuthor>
{
    public void Configure(EntityTypeBuilder<BookAuthor> builder)
    {
        builder.Property(wb => wb.Id)
            .HasColumnName($"{nameof(BookAuthor)}Id");

        builder.Property(wb => wb.WrittenWhen)
            .IsRequired();

        builder.HasIndex(wb => new { wb.ProductId, wb.AuthorId })
            .IsUnique();

        builder.HasOne(wb => wb.Product)
            .WithMany(p => p.BookAuthors)
            .HasForeignKey(wb => wb.ProductId);

        builder.HasOne(wb => wb.Author)
            .WithMany(a => a.AuthorBooks)
            .HasForeignKey(wb => wb.AuthorId);

        builder.HasOne(t => t.LastEditedByUser)
            .WithMany()
            .HasForeignKey(t => t.LastEditedBy);

        builder.HasOne(t => t.CreatedByUser)
            .WithMany()
            .HasForeignKey(t => t.CreatedBy);
    }
}
