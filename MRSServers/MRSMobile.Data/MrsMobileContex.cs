using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MRSMobile.Data.Models;

namespace MRSMobile.Data
{
    public class MrsMobileContex : IdentityDbContext<MrsUser, IdentityRole<string>, string>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(GetConnectionString());
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<MrsLocation>()
                .HasOne(x => x.Message)
                .WithOne(x => x.Location);

            base.OnModelCreating(builder);
        }

        public DbSet<MrsLocation> MrsLocations { get; set; }

        public DbSet<MrsMessage> MrsMessages { get; set; }

        public DbSet<MrsDevice> MrsDevices { get; set; }


        private static string GetConnectionString()
        {
            const string databaseName = "MrsMobileDb";


            return $@"Server=.\SQLEXPRESS;Database={databaseName};
                    Trusted_Connection=True;
                    Integrated Security=True;";
        }

    }
}
