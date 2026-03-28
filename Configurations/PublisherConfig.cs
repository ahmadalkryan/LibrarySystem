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
    public class PublisherConfig:IEntityTypeConfiguration<Publisher>
    {
        public void Configure(EntityTypeBuilder<Publisher> builder)
        {
            // Table Name
            builder.ToTable("Publishers");

            // Primary Key
            builder.HasKey(p => p.Id);

            // Properties Configuration
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasIndex(p => p.Name)
                .IsUnique()
                .HasDatabaseName("IX_Publishers_Name");

            builder.Property(p => p.Address)
                .HasMaxLength(500);

            builder.Property(p => p.ContactEmail)
                .HasMaxLength(100);

            builder.Property(p => p.ContactPhone)
                .HasMaxLength(20);

        }

    }
}
