using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ReadingRoomStore.Models
{
    public partial class ReadingRoomDBContext : DbContext
    {
        public ReadingRoomDBContext()
        {
        }

        public ReadingRoomDBContext(DbContextOptions<ReadingRoomDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Donator> Donators { get; set; }

        public virtual DbSet<test> Tests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ReadingRoomDB");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.BooksId);

                entity.ToTable("books");
            });

            //modelBuilder.Entity<Donator>(entity =>
            //{
            //    entity.ToTable("Donators");
            //});

            //modelBuilder.Entity<test>(entity =>
            //{
            //    entity.ToTable("Tests");
            //});

            base.OnModelCreating(modelBuilder);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
