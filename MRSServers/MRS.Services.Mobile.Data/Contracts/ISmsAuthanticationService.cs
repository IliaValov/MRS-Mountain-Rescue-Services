using System.Threading.Tasks;

namespace MRS.Services.Mobile.Data.Contracts
{
    public interface ISmsAuthanticationService
    {
        Task<string> AddSmsTokenAsync(string userId, string verificationCode);
    }
}
