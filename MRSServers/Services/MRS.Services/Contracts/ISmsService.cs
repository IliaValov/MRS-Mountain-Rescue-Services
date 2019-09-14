using System.Threading.Tasks;

namespace MRS.Services.Contracts
{
    public interface ISmsService
    {
        Task<string> SendSms(string accountSid, string authToken, string fromPhoneNumber, string toPhoneNumber);
    }
}
