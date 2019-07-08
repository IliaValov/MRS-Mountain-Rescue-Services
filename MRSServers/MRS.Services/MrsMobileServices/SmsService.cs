using MRS.Services.MrsMobileServices.Contracts;
using MRSMobile.Data;
using MRSMobile.Data.Models;
using System;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace MRS.Services.MrsMobileServices
{
    public class SmsService : ISmsService
    {
        private readonly MrsMobileDbContext context;

        public SmsService(MrsMobileDbContext context)
        {
            this.context = context;
        }


        public async Task<string> SendSms(string accountSid, string authToken, string fromPhoneNumber, string toPhoneNumber, string contry)
        {
            var token = "";

            try
            {
                var random = new Random();

                string authanticationCode = random.Next(1111, 9999).ToString();

                string phoneNumber = toPhoneNumber.Substring(1, toPhoneNumber.Length);

                TwilioClient.Init(accountSid, authToken);

                await MessageResource.CreateAsync(
                     body: "Join Earth's mightiest heroes. Like Kevin Bacon.",
                     from: new Twilio.Types.PhoneNumber(fromPhoneNumber),
                     to: new Twilio.Types.PhoneNumber("+359" + phoneNumber)
                 );

                 token = await AddSmsAuthantication(authanticationCode);
            }
            catch (Exception ex)
            {
                return "";
            }

            return token;
        }

        private async Task<string> AddSmsAuthantication(string authanticationCode)
        {
            var smsAuthantication = new MrsMobileSmsAuthantication();

            smsAuthantication.AuthanticationCode = authanticationCode;
            var token = smsAuthantication.Token = Guid.NewGuid().ToString();

            this.context.MobileSmsAuthantications.Add(smsAuthantication);
            await this.context.SaveChangesAsync();

            return token;

        }
    }
}
