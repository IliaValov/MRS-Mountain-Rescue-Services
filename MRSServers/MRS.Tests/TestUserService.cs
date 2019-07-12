using Microsoft.EntityFrameworkCore;
using MRS.Common.Mapping;
using MRS.Models.MRSMobileModels.BindingModels.Location;
using MRS.Services.MrsMobileServices;
using MRS.Services.MrsMobileServices.Contracts;
using MRSMobile.Data;
using MRSMobile.Data.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Tests
{
    public class TestUserService
    {
        private async Task<MrsMobileDbContext> GetDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<MrsMobileDbContext>()
                                    .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new MrsMobileDbContext(optionsBuilder.Options);

            foreach (var user in GetUsers())
            {
                await dbContext.Users.AddAsync(user);
            }

            await dbContext.SaveChangesAsync();
            return dbContext;
        }

        private IUserService GetUserService(MrsMobileDbContext dbContext)
        {
            return new UserService(dbContext);
        }

        private List<MrsMobileUser> GetUsers()
        {
            var users = new List<MrsMobileUser>
            {
                new MrsMobileUser { UserName = "0888014990", Email = "0888014990" },
                new MrsMobileUser { UserName = "0887512119", Email = "0887512119" },
                new MrsMobileUser { UserName = "0874322553", Email = "0874322553" },
            };

            return users;
        }

        [SetUp]
        public void Setup()
        {
            AutoMapperConfig.RegisterMappings(typeof(LocationCreateBindingModel).GetTypeInfo().Assembly);
        }

        [Test]
        public async Task GetAllUsers_WithCorrectData_ShouldReturnAllUsers()
        {
            //Arrange
            var dbContext = await this.GetDbContext();

            var userService = this.GetUserService(dbContext);

            //Act
            var actualResult = await userService.All<MrsMobileUser>();

            var expectedResult = GetUsers();
            //Assert

            Assert.IsTrue(await actualResult.CountAsync() == expectedResult.Count);


        }
    }
}