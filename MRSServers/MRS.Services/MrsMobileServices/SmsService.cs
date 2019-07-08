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


        public async Task<string> SendSms(string accountSid, string authToken, string fromPhoneNumber, string toPhoneNumber, string userId)
        {
            var token = "";

            try
            {
                var random = new Random();

                string authanticationCode = random.Next(1111, 9999).ToString();

                string phoneNumber = toPhoneNumber.Substring(1, toPhoneNumber.Length - 1);

                TwilioClient.Init(accountSid, authToken);

                await MessageResource.CreateAsync(
                     body: $"Enter this code to verify your phone number {authanticationCode}",
                     from: new Twilio.Types.PhoneNumber(fromPhoneNumber),
                     to: new Twilio.Types.PhoneNumber("+359" + phoneNumber)
                 );

                 token = await AddSmsAuthantication(authanticationCode, userId);
            }
            catch (Exception ex)
            {
                return "";
            }

            return token;
        }

        private async Task<string> AddSmsAuthantication(string authanticationCode, string userId)
        {
            var smsAuthantication = new MrsMobileSmsAuthantication
            {
                AuthanticationCode = authanticationCode,
                Token = Guid.NewGuid().ToString(),
                UserId = userId
            };

            this.context.MobileSmsAuthantications.Add(smsAuthantication);
            await this.context.SaveChangesAsync();

            return smsAuthantication.Token;
        }
    }
}
