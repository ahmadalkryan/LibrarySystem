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
    public class AuthorConfig:IEntityTypeConfiguration<Author>
    {

        public void Configure(EntityTypeBuilder<Author> builder)
        {
            // Table Name
            builder.ToTable("Authors");

            // Primary Key
            builder.HasKey(a => a.Id);

            // Properties Configuration
            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(a => a.Nationality)
                .HasMaxLength(100);

            builder.Property(a => a.Biography)
                .HasMaxLength(2000);

            builder.Property(a => a.BirthDate);
            builder.Property(a => a.DeathDate);
            // Indexes
            //builder.HasIndex(a => a.Name)
            //    .HasDatabaseName("IX_Authors_Name");
        }










    }
}
