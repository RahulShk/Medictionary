using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Medictionary.Models;

namespace Medictionary.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);
            modelbuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "admin", NormalizedName = "Admin" });

        }
        public DbSet<Industry> Industries { get; set; }
    }
}
