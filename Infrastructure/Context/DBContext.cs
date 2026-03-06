using Domain.Entities;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context
{
    public class DBContext:DbContext
    {

        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }
        #region
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Author> Authors { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<BorrowingRecord> BorrowingRecords { get; set; }
        public virtual DbSet<User> Users { get; set; }
         
        public virtual DbSet<Member> Members { get; set; }

        public virtual DbSet<BookCopy> BookCopies { get; set; }


        public virtual DbSet<Publisher> Publishers { get; set; }
        #endregion


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Fluent API configurations can be added here if needed


            // تطبيق جميع Configurations
            modelBuilder.ApplyConfiguration(new AuthorConfig());
            modelBuilder.ApplyConfiguration(new BookConfig());
            modelBuilder.ApplyConfiguration(new BookCopyConfig());
            modelBuilder.ApplyConfiguration(new BorrowingRecordConfig());
            modelBuilder.ApplyConfiguration(new CategoryConfig());
            modelBuilder.ApplyConfiguration(new MemberConfig());
            modelBuilder.ApplyConfiguration(new PublisherConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());










        }











    }
}
