using MRS.Mobile.Data;
using MRS.Mobile.Data.Models;
using MRS.Services.Mobile.Data.Contracts;
using System.Threading.Tasks;

namespace MRS.Services.Mobile.Data
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

            await dbContext.MrsDevices.AddAsync(newDevice);
            await dbContext.SaveChangesAsync();

            return newDevice.Id;
        }
    }
}
