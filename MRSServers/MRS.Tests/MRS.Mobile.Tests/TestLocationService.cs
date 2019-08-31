using AutoMapper;
using MRS.Common.Mapping;
using MRS.Mobile.Data;
using MRS.Mobile.Data.Models;
using MRS.Services.Mobile.Data;
using MRS.Services.Mobile.Data.Contracts;
using MRS.Tests.Common;
using MRS.Tests.MRS.Mobile.Tests.TestEntities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRS.Tests.MRS.Mobile.Tests
{
    public class TestLocationService
    {
        private ILocationService GetLocationService(MrsMobileDbContext dbContext)
        {

            return new LocationService(dbContext);
        }

        private MrsMobileUser GetDummyUser()
        {
            return new MrsMobileUser
            {
                UserName = "0888014990",



            };
        }

        private List<MrsMobileLocation> GetDummyLocations()
        {
            var locations = new List<MrsMobileLocation>
            {
                     new MrsMobileLocation() {
                            Latitude = 41,
                            Longitude = 76,
                            Altitude = 10,
                    },
                      new MrsMobileLocation() {
                            Latitude = 42,
                            Longitude = 654,
                            Altitude = 10,
                    },
                      new MrsMobileLocation() {
                            Latitude = 43,
                            Longitude = 56,
                            Altitude = 10,
                    }
            };


            return locations;
        }

        private async Task SeedUserAsync(MrsMobileDbContext context)
        {
            await context.Users.AddAsync(GetDummyUser());
            await context.SaveChangesAsync();
        }

        private async Task SeedDataAsync(MrsMobileDbContext context)
        {

            foreach (var location in GetDummyLocations())
            {
                var userId = context.Users.FirstOrDefault().Id;
                location.UserId = userId;
                await context.Locations.AddAsync(location);
            }
            await context.SaveChangesAsync();
        }

        [Test]
        public async Task AddLocationAsync_WithCorrectData_ShouldAddLocationsToTheDb()
        {
            string errorMessagePrefix = "LocationService AddLocationAsync() method does not work properly.";

            //Arrange
            var dbContext = MRSMobileDbContextInMemoryFactory.InitializeMobileContext();
            await SeedUserAsync(dbContext);

            var locationService = GetLocationService(dbContext);

            //Act

            var expectedResults = GetDummyLocations();

            foreach (var location in expectedResults)
            {
                var userId = dbContext.Users.FirstOrDefault().Id;
                location.UserId = userId;
                await locationService.AddLocationAsync(location);
            }

            var actualResults = dbContext.Locations.To<LocationTestEntity>().ToList();

            //Assert
            Assert.IsTrue(actualResults.Count() == expectedResults.Count, errorMessagePrefix + " " + "messages are not returned properly.");

        }

        [Test]
        public async Task GetByDateAsync_WithCorrectData_ShouldGetAllForTheCurrentDate()
        {
            string errorMessagePrefix = "LocationService AddLocationAsync() method does not work properly.";

            //Arrange
            var dbContext = MRSMobileDbContextInMemoryFactory.InitializeMobileContext();
            await SeedUserAsync(dbContext);
            await SeedDataAsync(dbContext);

            var locationService = GetLocationService(dbContext);

            //Act

            var actualResults = (await locationService.GetByDateAsync<LocationTestEntity>(DateTime.UtcNow)).OrderBy(x => x.CreatedOn).ToList();

            var expectedResults = Mapper.Map<List<LocationTestEntity>>(GetDummyLocations()).OrderBy(x => x.CreatedOn).ToList();

            Assert.IsTrue(actualResults.Count == expectedResults.Count, errorMessagePrefix + " " + "locations are not returned properly.");

            for (int i = 0; i < expectedResults.Count; i++)
            {
                var actualResult = actualResults[0];
                var expectedResult = expectedResults[0];

                Assert.IsTrue(actualResult.Longitude == expectedResult.Longitude, errorMessagePrefix + " " + "locations are not returned properly.");

            }
        }

        [Test]
        public async Task GetByDateAsync_WithZeroData_ShouldGetAllForTheCurrentDate()
        {
            string errorMessagePrefix = "LocationService AddLocationAsync() method does not work properly.";

            //Arrange
            var dbContext = MRSMobileDbContextInMemoryFactory.InitializeMobileContext();
            var locationService = GetLocationService(dbContext);

            //Act

            var actualResults = (await locationService.GetByDateAsync<LocationTestEntity>(DateTime.UtcNow)).OrderBy(x => x.CreatedOn).ToList();

            var expectedResults = Mapper.Map<List<LocationTestEntity>>(GetDummyLocations()).OrderBy(x => x.CreatedOn).ToList();

            Assert.IsTrue(actualResults.Count == 0, errorMessagePrefix + " " + "locations are not returned properly.");  
        }

        [Test]
        public async Task GetAllAsync_WithCorrectData_ShouldGetAllLocations()
        {
            string errorMessagePrefix = "LocationService AddLocationAsync() method does not work properly.";

            //Arrange
            var dbContext = MRSMobileDbContextInMemoryFactory.InitializeMobileContext();
            await SeedUserAsync(dbContext);
            await SeedDataAsync(dbContext);

            var locationService = GetLocationService(dbContext);

            //Act

            var actualResults = (await locationService.GetAllAsync<LocationTestEntity>()).OrderBy(x => x.CreatedOn).ToList();

            var expectedResults = Mapper.Map<List<LocationTestEntity>>(GetDummyLocations()).OrderBy(x => x.CreatedOn).ToList();

            Assert.IsTrue(actualResults.Count == expectedResults.Count, errorMessagePrefix + " " + "locations are not returned properly.");

            for (int i = 0; i < expectedResults.Count; i++)
            {
                var actualResult = actualResults[0];
                var expectedResult = expectedResults[0];

                Assert.IsTrue(actualResult.Longitude == expectedResult.Longitude, errorMessagePrefix + " " + "locations are not returned properly.");

            }
        }

        [Test]
        public async Task GetAllAsync_WithZeroData_ShouldGetAllLocations()
        {
            string errorMessagePrefix = "LocationService AddLocationAsync() method does not work properly.";

            //Arrange
            var dbContext = MRSMobileDbContextInMemoryFactory.InitializeMobileContext();

            var locationService = GetLocationService(dbContext);

            //Act

            var actualResults = (await locationService.GetAllAsync<LocationTestEntity>()).OrderBy(x => x.CreatedOn).ToList();

            var expectedResults = Mapper.Map<List<LocationTestEntity>>(GetDummyLocations()).OrderBy(x => x.CreatedOn).ToList();

            Assert.IsTrue(actualResults.Count == 0, errorMessagePrefix + " " + "locations are not returned properly.");

        }
    }
}
