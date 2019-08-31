using Microsoft.EntityFrameworkCore;
using MRS.Mobile.Data;
using MRS.Spa.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MRS.Tests.Common
{
    public class MRSMobileDbContextInMemoryFactory
    {
        public static MrsMobileDbContext InitializeMobileContext()
        {
            var options = new DbContextOptionsBuilder<MrsMobileDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            return new MrsMobileDbContext(options);
        }

        public static MrsSpaDbContext InitializeSpaContext()
        {
            var options = new DbContextOptionsBuilder<MrsSpaDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            return new MrsSpaDbContext(options);
        }
    }
}
