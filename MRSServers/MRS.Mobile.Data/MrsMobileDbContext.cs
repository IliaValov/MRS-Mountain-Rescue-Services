using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MRS.Common;
using MRS.Mobile.Data.Models;

namespace MRS.Mobile.Data
{
    public class MrsMobileDbContext : IdentityDbContext<MrsMobileUser, MrsMobileRole, string>
    {
        public MrsMobileDbContext(DbContextOptions options) : base(options)
        {
        }

        public MrsMobileDbContext()
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(GlobalConstants.MrsMobileDbConnectionString);
        //}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<MrsMobileLocation>()
                .HasOne(x => x.Message)
                .WithOne(x => x.Location)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<MrsMobileMessage>()
                .HasOne(x => x.User)
                .WithMany(x => x.Messages)
                .OnDelete(DeleteBehavior.Restrict);

            //builder.Entity<MrsUser>()
            //    .HasMany(e => e.Claims)
            //    .WithOne()
            //    .HasForeignKey(e => e.UserId)
            //    .IsRequired()
            //    .OnDelete(DeleteBehavior.Restrict);

            //builder.Entity<MrsUser>()
            //    .HasMany(e => e.Logins)
            //    .WithOne()
            //    .HasForeignKey(e => e.UserId)
            //    .IsRequired()
            //    .OnDelete(DeleteBehavior.Restrict);

            //builder.Entity<MrsUser>()
            //    .HasMany(e => e.Roles)
            //    .WithOne()
            //    .HasForeignKey(e => e.UserId)
            //    .IsRequired()
            //    .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }

        public DbSet<MrsMobileLocation> MrsLocations { get; set; }

        public DbSet<MrsMobileMessage> MrsMessages { get; set; }

        public DbSet<MrsMobileDevice> MrsDevices { get; set; }

        public DbSet<MrsMobileSmsAuthantication> MobileSmsAuthantications { get; set; }

    }
}
