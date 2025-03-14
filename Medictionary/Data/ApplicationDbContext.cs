﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Medictionary.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Medictionary.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Industry> Industries { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<StockiestMedicine> StockiestMedicines { get; set; }
        public DbSet<StockiestTransactionRecord> StockiestTransactionRecords { get; set; }
        public DbSet<StockiestRequest> StockiestRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>().HasData
            (
                new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "User", NormalizedName = "USER" },
                new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Stockiest", NormalizedName = "STOCKIEST" }
            );

            modelBuilder.Entity<StockiestMedicine>()
                .HasKey(sm => new { sm.StockiestID, sm.MedicineID });

            modelBuilder.Entity<StockiestMedicine>()
                .HasOne(sm => sm.Stockiest)
                .WithMany()
                .HasForeignKey(sm => sm.StockiestID);

            modelBuilder.Entity<StockiestMedicine>()
                .HasOne(sm => sm.Medicine)
                .WithMany()
                .HasForeignKey(sm => sm.MedicineID);

            modelBuilder.Entity<Medicine>()
                .HasOne(m => m.Industry)
                .WithMany(i => i.Medicines)
                .HasForeignKey(m => m.IndustryID);

            modelBuilder.Entity<StockiestTransactionRecord>()
                .HasOne(tr => tr.Medicine)
                .WithMany()
                .HasForeignKey(tr => tr.MedicineID);

            modelBuilder.Entity<StockiestTransactionRecord>()
                .HasOne(tr => tr.Stockiest)
                .WithMany()
                .HasForeignKey(tr => tr.StockiestID);

            modelBuilder.Entity<StockiestRequest>()
                .HasOne(sr => sr.Stockiest)
                .WithMany()
                .HasForeignKey(sr => sr.StockiestID);

            modelBuilder.Entity<StockiestRequest>()
                .HasOne(sr => sr.Industry)
                .WithMany()
                .HasForeignKey(sr => sr.IndustryID);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }
    }
}