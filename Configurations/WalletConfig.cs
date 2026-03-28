using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class WalletConfig: IEntityTypeConfiguration<Wallet> 
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.ToTable("Wallets");

            builder.HasKey(x => x.Id);

            //builder.Property(b => b.Balance);

            //builder.Property(b => b.TotalSpent);

            //builder.Property(b => b.TotalCharge);

            //builder.Property(v=>v.MaxBalance);

            //builder.Property(v => v.MinBalance);

            builder.Property(b => b.Balance).HasPrecision(18, 2);
            builder.Property(b => b.TotalSpent).HasPrecision(18, 2);
            builder.Property(b => b.TotalCharge).HasPrecision(18, 2);
            builder.Property(v => v.MaxBalance).HasPrecision(18, 2);
            builder.Property(v => v.MinBalance).HasPrecision(18, 2);

            builder.Property(v => v.CreatedAt).HasDefaultValueSql("GETDATE()");
            builder.Property(v => v.LastTransactionAt).IsRequired(false); // ✅ nullable


            builder.Property(v => v.IsActive).HasDefaultValue(true);

          

            builder.HasOne(bc => bc._member).WithOne(c=>c._Wallet)
                .HasForeignKey<Wallet>(x=>x._memberId).OnDelete(DeleteBehavior.Restrict);
                  
            




        }


    }
}
