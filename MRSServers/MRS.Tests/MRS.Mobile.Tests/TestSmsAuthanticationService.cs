using MRS.Services.Mobile.Data;
using MRS.Spa.Data;
using MRS.Spa.Data.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRS.Tests.Common
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class TestSmsAuthanticationService
    {
        public TestSmsAuthanticationService()
        {
            MapperInitializer.InitializeMapper();
        }

        private MrsSpaUser GetDummyUser()
        {
            return new MrsSpaUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "Administrator"
            };
        }

        private async Task SeedUserAsync(MrsSpaDbContext context)
        {
            await context.Users.AddAsync(GetDummyUser());

            await context.SaveChangesAsync();
        }


        [Test]
        public async Task AddAuthenticationToken_ShouldAddAndReturnAuthenticationToken()
        {
            const string dummyVerificationCode = "00000";

            var dbContext = MRSMobileDbContextInMemoryFactory.InitializeMobileContext();

            var smsService = new SmsAuthanticationService(dbContext);

            await smsService.AddSmsTokenAsync(this.GetDummyUser().Id, dummyVerificationCode);

            Assert.IsTrue(dbContext.MobileSmsAuthantications.Count() == 1);
        }
    }
}
