using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace AnagramSolver.EF.DatabaseFirst.Models
{
    public partial class AnagramDBContext : DbContext
    {
        public AnagramDBContext()
        {
        }

        public AnagramDBContext(DbContextOptions<AnagramDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CachedWord> CachedWords { get; set; }
        public virtual DbSet<UserLog> UserLogs { get; set; }
        public virtual DbSet<Word> Words { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LT-LIT-SC-0505;Database=AnagramDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<CachedWord>(entity =>
            {
                entity.ToTable("CachedWord");

                entity.Property(e => e.SearchingWord)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Anagram)
                    .WithMany(p => p.CachedWords)
                    .HasForeignKey(d => d.AnagramId)
                    .HasConstraintName("FK_CachedWord_Word");
            });

            modelBuilder.Entity<UserLog>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_UserLog_1");

                entity.ToTable("UserLog");

                entity.Property(e => e.Ip)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SearchTime).HasColumnType("datetime");

                entity.Property(e => e.SearchingWord)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Word>(entity =>
            {
                entity.ToTable("Word");

                entity.Property(e => e.Word1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Word");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
