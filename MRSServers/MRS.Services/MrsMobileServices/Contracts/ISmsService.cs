using System.Threading.Tasks;

namespace MRS.Services.MrsMobileServices.Contracts
{
    public interface ISmsService
    {
         Task<string> SendSms(string accountSid, string authToken, string fromPhoneNumber, string toPhoneNumber, string userId);
    }
}
