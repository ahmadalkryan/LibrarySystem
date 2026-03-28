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
    public class UserConfig:IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Table Name
            builder.ToTable("Users");

            // Primary Key
            builder.HasKey(u => u.Id);

            // Properties Configuration
            builder.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(150)
               ; 

            builder.HasIndex(u => u.Username)
                .IsUnique()
                .HasDatabaseName("IX_Users_Username");

            builder.Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(u => u.Role)
                .IsRequired()
                .HasMaxLength(50);

        }
    }
}
