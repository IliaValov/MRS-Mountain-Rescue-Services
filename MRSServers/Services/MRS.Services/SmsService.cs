using MRS.Mobile.Data;
using MRS.Mobile.Data.Models;
using MRS.Services.Contracts;
using System;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace MRS.Services
{
    public class SmsService : ISmsService
    {
        private readonly MrsMobileDbContext context;

        public SmsService(MrsMobileDbContext context)
        {
            this.context = context;
        }


        public async Task<string> SendSms(string accountSid, string authToken, string fromPhoneNumber, string toPhoneNumber)
        {
            string verificationCode = "";

            try
            {
                var random = new Random();

                //string authanticationCode = random.Next(1111, 9999).ToString();
                verificationCode = "4444";


                string phoneNumber = toPhoneNumber.Substring(1, toPhoneNumber.Length - 1);

                //TwilioClient.Init(accountSid, authToken);

                //await MessageResource.CreateAsync(
                //     body: $"Enter this code to verify your phone number {authanticationCode}",
                //     from: new Twilio.Types.PhoneNumber(fromPhoneNumber),
                //     to: new Twilio.Types.PhoneNumber("+359" + phoneNumber)
                // );


            }
            catch (Exception ex)
            {
                return "";
            }

            return verificationCode;
        }
    }
}
