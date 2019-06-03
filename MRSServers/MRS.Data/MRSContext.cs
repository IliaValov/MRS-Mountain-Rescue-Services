using MRS.Data.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MRS.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>

    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("Server=DESKTOP-RAEJK12\\SQLEXPRESS;Database=MRS;Trusted_Connection=True;");
        }

        public DbSet<MrsDevice> Devices { get; set; }
        public DbSet<MrsLocation> Locations { get; set; }
        public DbSet<MrsMessage> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
         
        }

    }
}
