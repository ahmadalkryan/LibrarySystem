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
    public class WalletTransactionConfig:IEntityTypeConfiguration<WalletTransaction>
    {
        public void Configure(EntityTypeBuilder<WalletTransaction> builder)
        {
            // Table Name
            builder.ToTable("WalletTransactions");

            // Primary Key
            builder.HasKey(p => p.Id);






            // Properties Configuration
            builder.Property(p => p.Amount).HasPrecision(18, 2);
            builder.Property(p => p.BalanceBefore).HasPrecision(18, 2);
            builder.Property(p => p.BalanceAfter).HasPrecision(18, 2);



            builder.Property(p => p.TransactionNumber)
             ;

            builder.Property(p => p.TransactionType).
                IsRequired() 
                .HasMaxLength(200);



            builder.Property(p => p.Description).HasMaxLength(500);
            builder.Property(p => p.Notes).HasMaxLength(500);

            builder.Property(p => p.Status)
                 .IsRequired()
                 .HasMaxLength(50)
                 .HasDefaultValue("Completed");


            builder.Property(p => p.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");







            builder.HasOne(b => b._wallet)
              .WithMany(a => a._walletTransactions)
              .HasForeignKey(b => b._walletId)
              .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(b => b.CreatedByUser)
              .WithMany(a => a._walletTransactions)
              .HasForeignKey(b => b.CreatedByUserId)
              .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(b => b._member)
             .WithMany(a => a._walletTransactions)
             .HasForeignKey(b => b._memberId)
             .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c._borrowingRecord)
                .WithMany(x => x._transactions)
                .HasForeignKey(x => x._borrowingRecordId).OnDelete(DeleteBehavior.Restrict);




            //builder.HasOne(wt => wt._borrowingRecord)
            //   .WithOne(br => br.WalletTransaction)
            //   .HasForeignKey<WalletTransaction>(wt => wt._borrowingRecordId)  // ✅ أضف هذا الحقل
            //   .OnDelete(DeleteBehavior.Restrict);



        }
    }
}
