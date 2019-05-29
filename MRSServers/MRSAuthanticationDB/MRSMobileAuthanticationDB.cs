using Microsoft.EntityFrameworkCore;
using MRSMobileAuthanticationDB.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MRSMobileAuthanticationDB
{
    public class MRSMobileAuthanticationDB : DbContext
    {
        public MRSMobileAuthanticationDB(DbContextOptions<MRSMobileAuthanticationDB> options) : base(options)
        {
        }

        protected MRSMobileAuthanticationDB()
        {
        }

        public DbSet<MobileAuthanticationQueue> MobileAuthanticationQueues { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:mrsdbserver.database.windows.net,1433;Initial Catalog=MRSDatabase;Persist Security Info=False;User ID=MRSAdmin;Password=Mrs@dmin32;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
