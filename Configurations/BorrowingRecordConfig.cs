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
    public  class BorrowingRecordConfig:IEntityTypeConfiguration<BorrowingRecord>
    {

        public void Configure(EntityTypeBuilder<BorrowingRecord> builder)
        {
            // Table Name
            builder.ToTable("BorrowingRecords");

            // Primary Key
            builder.HasKey(br => br.Id);

            // Properties Configuration
            builder.Property(br => br.TransactionNumber)
                .IsRequired()
                .HasMaxLength(50)
              ;

            builder.HasIndex(br => br.TransactionNumber)
                .IsUnique()
                .HasDatabaseName("IX_BorrowingRecords_TransactionNumber");

            builder.Property(br => br.BorrowDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(br => br.DueDate)
                .IsRequired();

            builder.Property(br => br.ReturnDate);

            builder.Property(br => br.BorrowingCost).HasPrecision(18, 2);
            builder.Property(br => br.FinePaid).HasPrecision(18, 2);

            // Relationships
            // BorrowingRecord -> Member (Many-to-One)
            builder.HasOne(br => br._member)
                .WithMany(m => m._borrowingRecords)
                .HasForeignKey(br => br._memberId)
                .OnDelete(DeleteBehavior.Restrict);

            // BorrowingRecord -> User (Many-to-One)
            builder.HasOne(br => br._user)
                .WithMany(u => u._borrowingRecords)
                .HasForeignKey(br => br._userId)
                .OnDelete(DeleteBehavior.Restrict);

            // BorrowingRecord -> BookCopy (Many-to-One)
            builder.HasOne(br => br._bookCopy)
                .WithMany(bc => bc._borrowRecords)
                .HasForeignKey(br => br._bookCopyId)
                .OnDelete(DeleteBehavior.Restrict);



            //builder.HasOne(br => br.WalletTransaction)
            //    .WithOne(wt => wt._borrowingRecord)
            //    .HasForeignKey<BorrowingRecord>(br => br._walletTransactionId)  // ✅ صحيح
            //    .OnDelete(DeleteBehavior.Restrict);


        }

    }
}
