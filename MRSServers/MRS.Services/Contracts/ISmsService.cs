namespace MRS.Services.Contracts
{
    public interface ISmsService
    {
        bool SendSms(string accountSid, string authToken, string fromPhoneNumber, string toPhoneNumber, string contry);
    }
}
