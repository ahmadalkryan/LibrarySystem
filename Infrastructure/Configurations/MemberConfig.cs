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
    public class MemberConfig : IEntityTypeConfiguration<Member>
    {

        public void Configure(EntityTypeBuilder<Member> builder)
        {
            // Table Name
            builder.ToTable("Members");

            // Primary Key
            builder.HasKey(m => m.Id);

            // Properties Configuration
            builder.Property(m => m.FullName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(m => m.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(m => m.Email)
                .IsUnique()
                .HasDatabaseName("IX_Members_Email");

            builder.Property(m => m.PhoneNumber)
                .HasMaxLength(20);

            builder.Property(m => m.Address)
                .HasMaxLength(500);

            builder.Property(m => m.MembershipType)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(m => m.MemberCode)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.HasIndex(m => m.MemberCode)
                .IsUnique()
                .HasDatabaseName("IX_Members_MemberCode");

          
        }
    }
}
