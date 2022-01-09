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
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRS.Tests.MRS.Mobile.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class TestDeviceService
    {
        private IDeviceService GetDeviceService(MrsMobileDbContext dbContext)
        {

            return new DeviceService(dbContext);
        }

        public TestDeviceService()
        {
            //MapperInitializer.InitializeMapper();
        }


        private List<MrsMobileDevice> GetDummyDevices()
        {
            var devices = new List<MrsMobileDevice>
            {
                     new MrsMobileDevice
                     {
                         Device = "Samsung"
                     },
                       new MrsMobileDevice
                     {
                         Device = "Lenovo"
                     },
                         new MrsMobileDevice
                     {
                         Device = "Huawei"
                     }
            };

            return devices;
        }

        private async Task SeedDataAsync(MrsMobileDbContext context)
        {
            foreach (var device in GetDummyDevices())
            {
                await context.Devices.AddAsync(device);
            }
            await context.SaveChangesAsync();
        }

        [Test]
        public async Task AddDeviceAsync_WithCorrectData_ShouldAddNewDevice()
        {
            string errorMessagePrefix = "DeviceService AddDeviceAsync() method does not work properly.";

            //Arrange
            var dbContext = MRSMobileDbContextInMemoryFactory.InitializeMobileContext();

            var deviceService = GetDeviceService(dbContext);

            //Act

            var expectedResults = Mapper.Map<List<DeviceTestEntity>>(GetDummyDevices());

            foreach (var device in expectedResults)
            {
                await deviceService.AddDeviceAsync(device.Device);
            }

            var actualResults = dbContext.Devices.To<DeviceTestEntity>().ToList();

            //Assert
            Assert.IsTrue(actualResults.Count() == expectedResults.Count, errorMessagePrefix + " " + "devices are not returned properly.");

        }
    }
}
