using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BankingApplication.DAL
{
    public partial class SmallOfficeContext : DbContext
    {
        public SmallOfficeContext()
        {
        }

        public SmallOfficeContext(DbContextOptions<SmallOfficeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BankAccount> BankAccounts { get; set; }
        public virtual DbSet<BankCustomer> BankCustomers { get; set; }
        public virtual DbSet<BankTransaction> BankTransactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=BankDB");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("TSS\\nikhil.satilal")
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<BankAccount>(entity =>
            {
                entity.Property(e => e.AccountNo).ValueGeneratedNever();

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.BankAccounts)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerID");
            });

            modelBuilder.Entity<BankCustomer>(entity =>
            {
                entity.Property(e => e.CustomerId).ValueGeneratedNever();
            });

            modelBuilder.Entity<BankTransaction>(entity =>
            {
                entity.HasKey(e => new { e.TransactionId, e.AccountNo });

                entity.HasOne(d => d.AccountNoNavigation)
                    .WithMany(p => p.BankTransactions)
                    .HasForeignKey(d => d.AccountNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccNo");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
