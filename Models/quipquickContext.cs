using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace quipquick_api.Models
{
    public partial class quipquickContext : DbContext
    {
        public quipquickContext()
        {
        }

        public quipquickContext(DbContextOptions<quipquickContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<CategoryJoke> CategoryJokes { get; set; } = null!;
        public virtual DbSet<Joke> Jokes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) { }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<CategoryJoke>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CategoryJoke");

                entity.Property(e => e.IdCategory).HasColumnName("idCategory");

                entity.Property(e => e.IdJoke).HasColumnName("idJoke");

                entity.HasOne(d => d.IdCategoryNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Category_Joke_Category");

                entity.HasOne(d => d.IdJokeNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdJoke)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Category_Joke_Joke");
            });

            modelBuilder.Entity<Joke>(entity =>
            {
                entity.ToTable("Joke");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.Likes).HasColumnName("likes");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
