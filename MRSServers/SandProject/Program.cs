using MRS.Common;
using System;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace SandProject
{
    class Program
    {
        static void Main(string[] args)
        {
            TestEnum testEnum = Enum.Parse<TestEnum>(GlobalConstants.NormalUserType);

            Console.WriteLine(testEnum.ToString("g"));
        }
    }
}
