using Microsoft.EntityFrameworkCore;
using Mrs.Spa.LocalData.Models;

namespace Mrs.Spa.LocalData
{
    public class MrsSpaLDContext : DbContext
    {
        public MrsSpaLDContext(DbContextOptions<MrsSpaLDContext> options) : base(options)
        {
        }

        public MrsSpaLDContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<MrsLDLocation>()
                .HasOne(x => x.Message)
                .WithOne(x => x.Location)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<MrsLDMessage>()
                .HasOne(x => x.User)
                .WithMany(x => x.Messages)
                .OnDelete(DeleteBehavior.Restrict);


            base.OnModelCreating(builder);
        }


        public DbSet<MrsLDUser> MrsLDUsers { get; set; }
        public DbSet<MrsLDLocation> MrsLDLocations { get; set; }
        public DbSet<MrsLDMessage> MrsLDMessages { get; set; }

    }
}
