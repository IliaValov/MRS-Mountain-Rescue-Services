using MRS.Services.MrsMobileServices.Contracts;
using MRSMobile.Data;
using MRSMobile.Data.Models;
using System.Threading.Tasks;

namespace MRS.Services.MrsMobileServices
{
    public class DeviceService : IDeviceService
    {
        private readonly MrsMobileDbContext dbContext;

        public DeviceService(MrsMobileDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<long> AddDevice(string device)
        {
            var newDevice = new MrsMobileDevice
            {
                Device = device
            };

            await this.dbContext.MrsDevices.AddAsync(newDevice);
            await this.dbContext.SaveChangesAsync();

            return newDevice.Id;
        }
    }
}
