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
    public class BookCopyConfig:IEntityTypeConfiguration<BookCopy>
    {
        public void Configure(EntityTypeBuilder<BookCopy> builder)
        {
            // Table Name
            builder.ToTable("BookCopies");

            // Primary Key
            builder.HasKey(bc => bc.Id);

            // Properties Configuration
            builder.Property(bc => bc.Barcode)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.HasIndex(bc => bc.Barcode)
                .IsUnique()
                .HasDatabaseName("IX_BookCopies_Barcode");

            builder.Property(bc => bc.Status)
                .IsRequired()
                .HasMaxLength(20)
                .HasConversion(
                    v => v.ToString(),
                    v => v);

            builder.Property(bc => bc.Location)
                .HasMaxLength(100);

            builder.Property(bc => bc.Notes)
                .HasMaxLength(500);

            builder.Property(bc => bc.AddedDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.HasOne(bc => bc._book)
                .WithMany(b => b._bookCopies)
                .HasForeignKey(bc => bc._bookId)
                .OnDelete(DeleteBehavior.Cascade);

               builder.HasOne(bc => bc._user).WithMany(u => u._bookCopies)
                .HasForeignKey(bc => bc._userId).OnDelete(DeleteBehavior.Restrict);
               ;





        }
    }
}
