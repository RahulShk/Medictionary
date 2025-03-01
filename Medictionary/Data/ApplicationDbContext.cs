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

        public DbSet<StockiestMedicine> StockiestMedicines { get; set; }
        public DbSet<Industry> Industries { get; set; }
        public DbSet<Medicine> Medicines { get; set; }

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
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }
    }
}