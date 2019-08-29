using Microsoft.EntityFrameworkCore;
using MRS.Common.Mapping;
using MRS.Mobile.Data;
using MRS.Mobile.Data.Models;
using MRS.Models.MRSMobileModels.BindingModels.Location;
using MRS.Models.MRSMobileModels.ViewModels.Account;
using MRS.Services.Mobile.Data;
using MRS.Services.Mobile.Data.Contracts;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Linq;
using MRS.Tests.MRS.Mobile.Tests.TestEntities;
using MRS.Tests.Common;
using AutoMapper;

namespace Tests
{
    public class TestUserService
    {
        public TestUserService()
        {
            MapperInitializer.InitializeMapper();
        }

        private IUserService GetUserService(MrsMobileDbContext dbContext)
        {

            return new UserService(dbContext);
        }

        private List<MrsMobileUser> GetDummyUsers()
        {
            var users = new List<MrsMobileUser>
            {
                new MrsMobileUser {
                    UserName = "0888014990",
                  

                },
                new MrsMobileUser { UserName = "0887512119"

                },
                new MrsMobileUser { UserName = "0874322553"

                }
            };


            return users;
        }


        private List<MrsMobileUser> GetDummyLocations()
        {
            var users = new List<MrsMobileUser>
            {
                new MrsMobileUser {
                    UserName = "0888014990",
                    Locations = new List<MrsMobileLocation>
            {
                     new MrsMobileLocation() {
                            Latitude = 41,
                            Longitude = 22,
                            Altitude = 10,
                    },
                      new MrsMobileLocation() {
                            Latitude = 41,
                            Longitude = 22,
                            Altitude = 10,
                    },
                      new MrsMobileLocation() {
                            Latitude = 41,
                            Longitude = 22,
                            Altitude = 10,
                    }
            }

                },
                new MrsMobileUser { UserName = "0887512119", Locations =new List<MrsMobileLocation>
            {
                     new MrsMobileLocation() {
                            Latitude = 41,
                            Longitude = 22,
                            Altitude = 10,
                    },
                      new MrsMobileLocation() {
                            Latitude = 41,
                            Longitude = 22,
                            Altitude = 10,
                    },
                      new MrsMobileLocation() {
                            Latitude = 41,
                            Longitude = 22,
                            Altitude = 10,
                    }
            }

                },
                new MrsMobileUser { UserName = "0874322553", Locations =new List<MrsMobileLocation>
            {
                     new MrsMobileLocation() {
                            Latitude = 41,
                            Longitude = 22,
                            Altitude = 10,
                    },
                      new MrsMobileLocation() {
                            Latitude = 41,
                            Longitude = 22,
                            Altitude = 10,
                    },
                      new MrsMobileLocation() {
                            Latitude = 41,
                            Longitude = 22,
                            Altitude = 10,
                    }
            }

                }
            };


            return users;
        }

        private async Task SeedData(MrsMobileDbContext context, bool seedDataWithLocations)
        {
            if (seedDataWithLocations)
            {
                foreach (var item in GetDummyLocations())
                {
                    await context.Users.AddAsync(item);
                }

                await context.SaveChangesAsync();
            }
            else
            {
                foreach (var item in GetDummyUsers())
                {
                    await context.Users.AddAsync(item);
                }
                await context.SaveChangesAsync();
            }

        }

        [Test]
        public async Task GetAllUsers_WithCorrectData_ShouldReturnAllUsers()
        {
            string errorMessagePrefix = "UserService GetAllAsync() method does not work properly.";

            //Arrange
            var dbContext = MRSMobileDbContextInMemoryFactory.InitializeContext();
            await SeedData(dbContext, false);

            var userService = this.GetUserService(dbContext);

            //Act
            var actualResults = (await userService.GetAllAsync<UserTestEntity>()).ToList();

            var expectedResults = Mapper.Map<List<UserTestEntity>>(GetDummyUsers());
            //Assert

            Assert.IsTrue(actualResults.Count() == expectedResults.Count);


            for (int i = 0; i < expectedResults.Count; i++)
            {
                var expectedEntry = expectedResults[i];
                var actualEntry = actualResults[i];

                Assert.True(expectedEntry.UserName == actualEntry.UserName, errorMessagePrefix + " " + "UserName is not returned properly.");
            }

        }

        [Test]
        public async Task GetAllUsers_WithZeroData_ShouldReturnEmptyResults()
        {
            string errorMessagePrefix = "UserService GetAllAsync() method does not work properly.";

            //Arrange
            var dbContext = MRSMobileDbContextInMemoryFactory.InitializeContext();

            var userService = this.GetUserService(dbContext);
            //Act
            var actualResults = (await userService.GetAllAsync<UserTestEntity>()).ToList();

            //Assert

            Assert.True(actualResults.Count == 0, errorMessagePrefix);
        }

        [Test]
        public async Task GetAllUsersWithLocationsWithDateAsync_WithCorrectData_ShouldReturnAllUsersForEveryUserShouldReturnHisLocationsForTheGivenDate()
        {
            string errorMessagePrefix = "UserService GetAllUsersWithLocationsWithDateAsync method does not work properly.";

            //Arrange
            var dbContext = MRSMobileDbContextInMemoryFactory.InitializeContext();
            await SeedData(dbContext, true);

            var userService = this.GetUserService(dbContext);
            //Act
            var actualResults = (await userService.GetAllUsersWithLocationsWithDateAsync<UserTestEntity>(DateTime.UtcNow)).OrderBy(x => x.CreatedOn).ToList();

            var expectedResults = Mapper.Map<List<UserTestEntity>>(GetDummyUsers()).OrderBy(x => x.CreatedOn).ToList();

            Assert.IsTrue(actualResults.Count() == expectedResults.Count);

            for (int i = 0; i < expectedResults.Count; i++)
            {
                var expectedEntry = expectedResults[i];
                var actualEntry = actualResults[i];

                Assert.True(expectedEntry.Locations.Count == actualEntry.Locations.Count, errorMessagePrefix + " " + "locations are not returned properly.");

                for (int j = 0; j < expectedEntry.Locations.Count; j++)
                {
                    var expectedLocationEntry = expectedEntry.Locations.ToList()[j];
                    var actualLocationEntry = actualEntry.Locations.ToList()[j];

                    Assert.True(expectedLocationEntry.Latitude == actualLocationEntry.Latitude, errorMessagePrefix + " " + "UserName is not returned properly.");
                }
            }
        }

        [Test]
        public async Task GetAllUsersWithLocationsWithDateAsync_WithZeroData_ShouldReturnEmptyResult()
        {
            string errorMessagePrefix = "UserService GetAllUsersWithLocationsWithDateAsync method does not work properly.";

            //Arrange
            var dbContext = MRSMobileDbContextInMemoryFactory.InitializeContext();
            await SeedData(dbContext, false);

            var userService = this.GetUserService(dbContext);


            //Act
            var actualResults = (await userService.GetAllUsersWithLocationsWithDateAsync<UserTestEntity>(DateTime.UtcNow)).OrderBy(x => x.CreatedOn).ToList();

            var expectedResults = Mapper.Map<List<UserTestEntity>>(GetDummyUsers()).OrderBy(x => x.CreatedOn).ToList();

            //Assert
            Assert.IsTrue(actualResults.Count() == expectedResults.Count);

            for (int i = 0; i < expectedResults.Count; i++)
            {
                var actualEntry = actualResults[i];

                Assert.True(actualEntry.Locations.Count == 0, errorMessagePrefix + " " + "locations are not returned properly.");


            }
        }

        [Test]
        public async Task ChangeUserConditionAsync_WithCorrectData_ShouldChangeFirstUserCondition()
        {
            string errorMessagePrefix = "UserService ChangeUserConditionAsync method does not work properly.";


            //Arrange
            var dbContext = MRSMobileDbContextInMemoryFactory.InitializeContext();
            await SeedData(dbContext, false);

            var userService = this.GetUserService(dbContext);


            //Act
            var userId = dbContext.Users.FirstOrDefault().Id;

            await userService.ChangeUserConditionAsync(userId, true);
            dbContext.SaveChanges();

            var actualResults = dbContext.Users.FirstOrDefault();

            Assert.True(actualResults.IsInDanger, errorMessagePrefix + " " + "condition is not returned properly.");


        }
    }
}