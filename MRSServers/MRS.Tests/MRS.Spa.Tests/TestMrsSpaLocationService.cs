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
    public class TestMrsSpaLocationService
    {
        private ILocationService GetLocationService(MrsSpaDbContext dbContext)
        {

            return new LocationService(dbContext);
        }

        private List<MrsSpaLocation> GetDummyLocations()
        {
            var locations = new List<MrsSpaLocation>
            {
                     new MrsSpaLocation() {
                            Latitude = 41,
                            Longitude = 76,
                            Altitude = 10,
                    },
                      new MrsSpaLocation() {
                            Latitude = 42,
                            Longitude = 654,
                            Altitude = 10,
                    },
                      new MrsSpaLocation() {
                            Latitude = 43,
                            Longitude = 56,
                            Altitude = 10,
                    }
            };


            return locations;
        }

        private MrsSpaPolygon GetDummyPolygon()
        {
            return new MrsSpaPolygon
            {
                Name = "Name"
            };
        }

        private async Task SeedPolygonAsync(MrsSpaDbContext context)
        {
            await context.Polygons.AddAsync(GetDummyPolygon());
            await context.SaveChangesAsync();
        }

        private async Task SeedDataAsync(MrsSpaDbContext context)
        {
            await SeedPolygonAsync(context);
            foreach (var location in GetDummyLocations())
            {
                var polygonId = context.Polygons.FirstOrDefault().Id;
                location.PolygonId = polygonId;
                await context.Locations.AddAsync(location);
            }
            await context.SaveChangesAsync();
        }

        [Test]
        public async Task AddLocationAsync_WithCorrectData_ShouldAddNewLocationToTheDataBase()
        {
            string errorMessagePrefix = "LocationService AddLocationAsync() method does not work properly.";

            //Arrange
            var dbContext = MRSMobileDbContextInMemoryFactory.InitializeSpaContext();

            var locationService = GetLocationService(dbContext);

            //Act

            var expectedResults = Mapper.Map<List<SpaLocationTestEntity>>(GetDummyLocations());

            foreach (var location in expectedResults)
            {
                await locationService.AddLocationAsync(location);
            }

            var actualResults = dbContext.Locations.To<SpaLocationTestEntity>().ToList();

            //Assert
            Assert.IsTrue(actualResults.Count() == expectedResults.Count, errorMessagePrefix + " " + "locations are not returned properly.");

        }

        [Test]
        public async Task GetLocationsByPolygonIdAsync_WithCorrectData_ShouldReturnAllLocationsForTheGivenPolygonId()
        {
            string errorMessagePrefix = "LocationService AddLocationAsync() method does not work properly.";

            //Arrange
            var dbContext = MRSMobileDbContextInMemoryFactory.InitializeSpaContext();
            await SeedDataAsync(dbContext);

            var locationService = GetLocationService(dbContext);


            //Act
            var polygonId = dbContext.Polygons.FirstOrDefault().Id;

            var actualResults = (await locationService.GetLocationsByPolygonIdAsync<SpaLocationTestEntity>(polygonId)).OrderBy(x => x.CreatedOn).ToList();


            var expectedResults = Mapper.Map<List<SpaLocationTestEntity>>(GetDummyLocations());

            Assert.IsTrue(actualResults.Count == expectedResults.Count, errorMessagePrefix + " " + "locations are not returned properly.");

            for (int i = 0; i < expectedResults.Count; i++)
            {
                var actualResult = actualResults[0];
                var expectedResult = expectedResults[0];

                Assert.IsTrue(actualResult.Longitude == expectedResult.Longitude, errorMessagePrefix + " " + "locations are not returned properly.");

            }
        }

        [Test]
        public async Task GetLocationsByPolygonIdAsync_WithZeroData_ShouldReturnAllLocationsForTheGivenPolygonId()
        {
            string errorMessagePrefix = "LocationService AddLocationAsync() method does not work properly.";

            //Arrange
            var dbContext = MRSMobileDbContextInMemoryFactory.InitializeSpaContext();
            await SeedPolygonAsync(dbContext);

            var locationService = GetLocationService(dbContext);


            //Act
            var polygonId = dbContext.Polygons.FirstOrDefault().Id;

            var actualResults = (await locationService.GetLocationsByPolygonIdAsync<SpaLocationTestEntity>(polygonId)).OrderBy(x => x.CreatedOn).ToList();;

            Assert.IsTrue(actualResults.Count == 0, errorMessagePrefix + " " + "locations are not returned properly.");

        }
    }
}
