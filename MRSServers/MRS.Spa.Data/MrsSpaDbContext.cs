using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MRS.Common;
using MRS.Spa.Data.Models;

namespace MRS.Spa.Data
{
    public class MrsSpaDbContext : IdentityDbContext<MrsSpaUser, MrsSpaRole, string>
    {
        public MrsSpaDbContext(DbContextOptions<MrsSpaDbContext> options) : base(options)
        {
        }

        public MrsSpaDbContext()
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(GlobalConstants.MrsSpaDbConnectionString);
        //}

        protected override void OnModelCreating(ModelBuilder builder)
        {


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
            //    .IsRequired()`    
            //    .OnDelete(DeleteBehavior.Restrict);

            //builder.Entity<MrsUser>()
            //    .HasMany(e => e.Roles)
            //    .WithOne()
            //    .HasForeignKey(e => e.UserId)
            //    .IsRequired()
            //    .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
