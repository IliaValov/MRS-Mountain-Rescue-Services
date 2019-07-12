using System.Threading.Tasks;

namespace MRS.Services.Mobile.Data.Contracts
{
    public interface IDeviceService
    {
        Task<long> AddDevice(string device);
    }
}
