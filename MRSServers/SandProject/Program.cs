using System;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace SandProject
{
    class Program
    {
        static void Main(string[] args)
        {
            // Find your Account Sid and Token at twilio.com/console
            // DANGER! This is insecure. See http://twil.io/secure
            const string accountSid = "ACe3446ad361a9622a1f8cb43ea1982b54";
            const string authToken = "5087a0d9b687b4f6c6a478ae2161d592";

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: "Join Earth's mightiest heroes. Like Kevin Bacon.",
                from: new Twilio.Types.PhoneNumber("+18327261858"),
                to: new Twilio.Types.PhoneNumber("+359888014990")
            );

            Console.WriteLine(message.Sid);
        }
    }
}
