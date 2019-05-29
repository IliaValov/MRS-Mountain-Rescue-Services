using Microsoft.EntityFrameworkCore;
using MRS.Data.Configurations;
using MRS.Models;

namespace MRS.Data
{
    public class MRSContext : DbContext
    {
        public MRSContext()
        {
        }

        public MRSContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:mrsdbserver.database.windows.net,1433;Initial Catalog=MRSDatabase;Persist Security Info=False;User ID=Mrsadmin;Password=Mrs@dmin32;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        public DbSet<MrsUser> MrsUsers { get; set; }
        public DbSet<MrsRole> MrsRoles { get; set; }
        public DbSet<MrsLocation> MrsLocations { get; set; }
        public DbSet<MrsUserRole> MrsUserRoles { get; set; }
        public DbSet<MrsUserDevice> MrsUserDevices { get; set; }
        public DbSet<MrsUserVerification> MrsUserVerifications { get; set; }
        public DbSet<MrsImergencyMessage> MrsImergencyMessages { get; set; }
        public DbSet<MrsUserAuthanticationToken> MrsUserAuthanticationTokens { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new MrsUserConfig());
           // builder.ApplyConfiguration(new MrsLocationConfig());
            builder.ApplyConfiguration(new MrsUserRoleConfig());
            builder.ApplyConfiguration(new MrsImergencyMessageConfig());
            builder.ApplyConfiguration(new MrsUserAuthanticationTokenConfig());
        }

    }
}
