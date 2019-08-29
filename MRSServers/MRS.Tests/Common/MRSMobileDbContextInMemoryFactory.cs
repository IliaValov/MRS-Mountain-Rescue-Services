using Microsoft.EntityFrameworkCore;
using MRS.Mobile.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MRS.Tests.Common
{
    public class MRSMobileDbContextInMemoryFactory
    {
        public static MrsMobileDbContext InitializeContext()
        {
            var options = new DbContextOptionsBuilder<MrsMobileDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            return new MrsMobileDbContext(options);
        }
    }
}
