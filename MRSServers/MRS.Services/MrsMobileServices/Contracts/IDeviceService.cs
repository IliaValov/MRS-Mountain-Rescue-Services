using System.Threading.Tasks;

namespace MRS.Services.MrsMobileServices.Contracts
{
    public interface IDeviceService
    {
         Task<long> AddDevice(string device);
    }
}
