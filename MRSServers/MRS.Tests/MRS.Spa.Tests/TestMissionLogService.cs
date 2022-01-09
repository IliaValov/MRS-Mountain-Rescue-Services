using AutoMapper;
using MRS.Common.Mapping;
using MRS.Services.Spa.Data;
using MRS.Services.Spa.Data.Contracts;
using MRS.Spa.Data;
using MRS.Spa.Data.Models;
using MRS.Tests.Common;
using MRS.Tests.MRS.Spa.Tests.TestEntities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace MRS.Tests.MRS.Spa.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class TestMissionLogService
    {

        private IMissionLogService GetMissionLogService(MrsSpaDbContext dbContext)
        {

            return new MissionLogService(dbContext);
        }

        private List<MrsSpaMissionLog> GetDummyMissionLogs()
        {
            var missionlogs = new List<MrsSpaMissionLog>
            {
                     new MrsSpaMissionLog
                     {
                         MissionName = "Name",
                         Details = "Details",
                         IsMissionSuccess = true,
                         PhoneNumber = "0888019999",

                     },
                       new MrsSpaMissionLog
                     {
                         MissionName = "Name1",
                         Details = "Details1",
                         IsMissionSuccess = false,
                         PhoneNumber = "0888018888",
                     },
                         new MrsSpaMissionLog
                     {
                         MissionName = "Name2",
                         Details = "Details2",
                         IsMissionSuccess = true,
                         PhoneNumber = "0888017777",
                     }
            };

            return missionlogs;
        }

        private MrsSpaUser GetDummyUser()
        {
            return new MrsSpaUser()
            {
                UserName = "Administrator"
            };
        }

        private async Task SeedUserAsync(MrsSpaDbContext context)
        {
            await context.Users.AddAsync(GetDummyUser());

            await context.SaveChangesAsync();
        }

        private async Task SeedDataAsync(MrsSpaDbContext context)
        {
            await SeedUserAsync(context);
            foreach (var missionLog in GetDummyMissionLogs())
            {
                var userId = context.Users.FirstOrDefault().Id;
                missionLog.UserId = userId;
                await context.MissionLogs.AddAsync(missionLog);
            }
            await context.SaveChangesAsync();
        }

        [Test]
        public async Task AddMissionLogAsync_WithCorrectData_ShouldAddMissionLogToTheDb()
        {
            string errorMessagePrefix = "MissionLogService AddMissionLog() method does not work properly.";

            //Arrange
            var dbContext = MRSMobileDbContextInMemoryFactory.InitializeSpaContext();

            var missionLogService = GetMissionLogService(dbContext);

            //Act

            var expectedResults = Mapper.Map<List<MissionLogTestEntity>>(GetDummyMissionLogs());

            foreach (var missionLog in expectedResults)
            {
                await missionLogService.AddMissionLogAsync(missionLog);
            }

            var actualResults = dbContext.MissionLogs.To<MissionLogTestEntity>().ToList();

            //Assert
            Assert.IsTrue(actualResults.Count() == expectedResults.Count, errorMessagePrefix + " " + "missionlogs are not returned properly.");

        }

        [Test]
        public async Task GetAllMissionLogsByUserIdAsync_WithCorrectData_ShouldGetAllMissionLogsForTheGivenUser()
        {

            string errorMessagePrefix = "MissionLogService AddMissionLog() method does not work properly.";

            //Arrange
            var dbContext = MRSMobileDbContextInMemoryFactory.InitializeSpaContext();
            await SeedDataAsync(dbContext);

            var missionLogService = GetMissionLogService(dbContext);


            //Act
            var userId = dbContext.Users.FirstOrDefault().Id;

            var actualResults = (await missionLogService.GetAllMissionLogsByUserIdAsync<MissionLogTestEntity>(userId)).OrderBy(x => x.CreatedOn).ToList();


            var expectedResults = Mapper.Map<List<MissionLogTestEntity>>(GetDummyMissionLogs());

            Assert.IsTrue(actualResults.Count == expectedResults.Count, errorMessagePrefix + " " + "missionslogs are not returned properly.");

            for (int i = 0; i < expectedResults.Count; i++)
            {
                var actualResult = actualResults[0];
                var expectedResult = expectedResults[0];

                Assert.IsTrue(actualResult.MissionName == expectedResult.MissionName, errorMessagePrefix + " " + "missionlogs are not returned properly.");

            }
        }

        [Test]
        public async Task GetAllMissionLogsByUserIdAsync_WithZeroData_ShouldGetAllMissionLogsForTheGivenUser()
        {

            string errorMessagePrefix = "MissionLogService AddMissionLog() method does not work properly.";

            //Arrange
            var dbContext = MRSMobileDbContextInMemoryFactory.InitializeSpaContext();

            var missionLogService = GetMissionLogService(dbContext);
            await SeedUserAsync(dbContext);

            //Act
            var userId = dbContext.Users.FirstOrDefault().Id;

            var actualResults = (await missionLogService.GetAllMissionLogsByUserIdAsync<MissionLogTestEntity>(userId)).OrderBy(x => x.CreatedOn).ToList();


            var expectedResults = Mapper.Map<List<MissionLogTestEntity>>(GetDummyMissionLogs());

            Assert.IsTrue(actualResults.Count == 0, errorMessagePrefix + " " + "missionslogs are not returned properly.");

        }
    }
}
