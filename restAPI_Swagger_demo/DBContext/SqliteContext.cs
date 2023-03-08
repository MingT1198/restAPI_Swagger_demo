using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using restAPI_Swagger_demo.Models;

namespace restAPI_Swagger_demo.DBContext
{
    public partial class SQLiteContext : DbContext
    {
        public SQLiteContext()
        {
        }

        public SQLiteContext(DbContextOptions<SQLiteContext> options)
            : base(options)
        {

        }

        public virtual DbSet<tBook> tBooks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tBook>(entity =>
            {
                entity.HasKey(e => new { e.Id })
                    .HasName("PK_Book");

                entity.ToTable("tBook");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
