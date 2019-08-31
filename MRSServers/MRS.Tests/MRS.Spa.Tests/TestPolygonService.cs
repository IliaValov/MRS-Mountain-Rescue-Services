using AutoMapper;
using MRS.Common.Mapping;
using MRS.Services.Spa.Data;
using MRS.Services.Spa.Data.Contracts;
using MRS.Spa.Data;
using MRS.Spa.Data.Models;
using MRS.Tests.Common;
using MRS.Tests.MRS.Spa.Tests.TestEntities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRS.Tests.MRS.Spa.Tests
{
    public class TestPolygonService
    {
        private IPolygonService GetPolygonService(MrsSpaDbContext dbContext)
        {
            return new PolygonService(dbContext);
        }

        private List<MrsSpaPolygon> GetDummyPolygons()
        {
            var polygons = new List<MrsSpaPolygon>
            {
                     new MrsSpaPolygon
                     {
                        Name="Polygon1",
                        

                     },
                       new MrsSpaPolygon
                     {
                        Name="Polygon2"
                     },
                         new MrsSpaPolygon
                     {
                        Name="Polygon3"
                     }
            };

            return polygons;
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
            foreach (var polygon in GetDummyPolygons())
            {
                var userId = context.Users.FirstOrDefault().Id;
                polygon.UserId = userId;
                await context.Polygons.AddAsync(polygon);
            }
            await context.SaveChangesAsync();
        }

        [Test]
        public async Task AddPolygonAsync_WithCorrectData_ShouldAddNewPolygonsToTheBase()
        {
            string errorMessagePrefix = "PolygonService AddPolygonAsync() method does not work properly.";

            //Arrange
            var dbContext = MRSMobileDbContextInMemoryFactory.InitializeSpaContext();

            var polygonService = GetPolygonService(dbContext);

            //Act

            var expectedResults = Mapper.Map<List<PolygonTestEntity>>(GetDummyPolygons());

            foreach (var missionLog in expectedResults)
            {
                await polygonService.AddPolygonAsync(missionLog);
            }

            var actualResults = dbContext.Polygons.To<PolygonTestEntity>().ToList();

            //Assert
            Assert.IsTrue(actualResults.Count() == expectedResults.Count, errorMessagePrefix + " " + "polygons are not returned properly.");

        }

        [Test]
        public async Task GetPolygonsByUserIdAsync_WithCorrectData_ShouldReturnAllPolygonsByUserId()
        {
            string errorMessagePrefix = "PolygonService AddPolygonAsync() method does not work properly.";

            //Arrange
            var dbContext = MRSMobileDbContextInMemoryFactory.InitializeSpaContext();
            await SeedDataAsync(dbContext);

            var polygonService = GetPolygonService(dbContext);


            //Act
            var userId = dbContext.Users.FirstOrDefault().Id;

            var actualResults = (await polygonService.GetPolygonsByUserIdAsync<PolygonTestEntity>(userId)).OrderBy(x => x.CreatedOn).ToList();


            var expectedResults = Mapper.Map<List<PolygonTestEntity>>(GetDummyPolygons());

            Assert.IsTrue(actualResults.Count == expectedResults.Count, errorMessagePrefix + " " + "polygons are not returned properly.");

            for (int i = 0; i < expectedResults.Count; i++)
            {
                var actualResult = actualResults[0];
                var expectedResult = expectedResults[0];

                Assert.IsTrue(actualResult.Name == expectedResult.Name, errorMessagePrefix + " " + "polygons are not returned properly.");

            }
        }

        [Test]
        public async Task GetPolygonsByUserIdAsync_WithZeroData_ShouldReturnAllPolygonsByUserId()
        {
            string errorMessagePrefix = "PolygonService AddPolygonAsync() method does not work properly.";

            //Arrange
            var dbContext = MRSMobileDbContextInMemoryFactory.InitializeSpaContext();
            await SeedUserAsync(dbContext);

            var polygonService = GetPolygonService(dbContext);


            //Act
            var userId = dbContext.Users.FirstOrDefault().Id;

            var actualResults = (await polygonService.GetPolygonsByUserIdAsync<PolygonTestEntity>(userId)).OrderBy(x => x.CreatedOn).ToList();


            var expectedResults = Mapper.Map<List<PolygonTestEntity>>(GetDummyPolygons());

            Assert.IsTrue(actualResults.Count == 0, errorMessagePrefix + " " + "polygons are not returned properly.");

        }
    }
}
