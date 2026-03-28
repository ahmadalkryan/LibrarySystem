using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class BookConfig:IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            // Table Name
            builder.ToTable("Books");

            // Primary Key
            builder.HasKey(b => b.Id);

            // Properties Configuration
            builder.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(b => b.ISBN)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);

            //builder.HasIndex(b => b.ISBN)
            //    .IsUnique()
            //    .HasDatabaseName("IX_Books_ISBN");

            builder.Property(b => b.Description)
                .HasMaxLength(2000);

            builder.Property(b => b.Language)
                .HasMaxLength(50);

            builder.Property(b => b.PageCount)
                .IsRequired();

            // Relationships
            // Book -> Author (Many-to-One)
            builder.HasOne(b => b._author)
                .WithMany(a => a._books)
                .HasForeignKey(b => b._authorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Book -> Publisher (Many-to-One)
            builder.HasOne(b => b._publisher)
                .WithMany(p => p._books)
                .HasForeignKey(b => b._publisherId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b => b._user)
                .WithMany(u => u._books)
                .HasForeignKey(b => b._userId)
                .OnDelete(DeleteBehavior.Restrict);

            // Book -> Category (Many-to-One)
            builder.HasOne(b => b._category)
                .WithMany(c => c._books)
                .HasForeignKey(b => b._categoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Book -> BookCopies (One-to-Many)
            //builder.HasMany(b => b._bookCopies)
            //    .WithOne(bc => bc.Book)
            //    .HasForeignKey(bc => bc.BookId)
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
