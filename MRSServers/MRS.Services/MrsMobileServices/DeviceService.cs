using MRS.Services.MrsMobileServices.Contracts;
using MRSMobile.Data;
using MRSMobile.Data.Models;

namespace MRS.Services.MrsMobileServices
{
    public class DeviceService : IDeviceService
    {
        private readonly MrsMobileDbContext dbContext;

        public DeviceService(MrsMobileDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public long AddDevice(string device)
        {
            var newDevice = new MrsMobileDevice
            {
                Device = device
            };

            this.dbContext.MrsDevices.Add(newDevice);
            this.dbContext.SaveChanges();

            return newDevice.Id;
        }
    }
}
