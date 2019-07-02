using MRS.Services.Contracts;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace MRS.Services
{
    public class SmsService : ISmsService
    {
        public bool SendSms(string accountSid, string authToken, string fromPhoneNumber ,string toPhoneNumber, string contry)
        {
            string phoneNumber = toPhoneNumber.Substring(1, toPhoneNumber.Length);

            TwilioClient.Init(accountSid, authToken);

            MessageResource.Create(
                body: "Join Earth's mightiest heroes. Like Kevin Bacon.",
                from: new Twilio.Types.PhoneNumber(fromPhoneNumber),
                to: new Twilio.Types.PhoneNumber("+359" + phoneNumber)
            );


            return true;
        }
    }
}
