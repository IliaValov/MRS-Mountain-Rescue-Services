﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MRSMobile.Data.Models;

namespace MRSMobile.Data
{
    public class MrsMobileDbContext : IdentityDbContext<MrsUser, MrsRole, string>
    {
        public MrsMobileDbContext(DbContextOptions options) : base(options)
        {
        }

        protected MrsMobileDbContext()
        {
        }

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

            builder
                .Entity<MrsMessage>()
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