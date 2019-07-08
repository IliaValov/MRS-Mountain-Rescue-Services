using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MRSWeb.Data.Models;

namespace MRSWeb.Data
{
    public class MrsWebDbContext : IdentityDbContext<MrsWebUser, MrsWebRole, string>
    {
        public MrsWebDbContext(DbContextOptions options) : base(options)
        {
        }

        protected MrsWebDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(GetConnectionString());
        }

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



        private static string GetConnectionString()
        {
            const string databaseName = "MrsWebDb";


            return $@"Server=.\SQLEXPRESS;Database={databaseName};
                    Trusted_Connection=True;
                    Integrated Security=True;";
        }
    }
}
