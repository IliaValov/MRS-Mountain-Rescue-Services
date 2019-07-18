using System.Threading.Tasks;
using AutoMapper;
using MRS.Mobile.Data;
using MRS.Mobile.Data.Models;
using MRS.Services.Mobile.Data.Contracts;

namespace MRS.Services.Mobile.Data
{
    public class SmsAuthanticationService : ISmsAuthanticationService
    {
        private readonly MrsMobileDbContext context;

        public SmsAuthanticationService(MrsMobileDbContext context)
        {
            this.context = context;
        }

        public async Task<string> AddSmsTokenAsync(string userId, string verificationCode)
        {
            var smsAuth = new MrsMobileSmsAuthantication
            {
                UserId = userId,
                AuthanticationCode = verificationCode
            };

            this.context.MobileSmsAuthantications.Add(Mapper.Map<MrsMobileSmsAuthantication>(smsAuth));
            await this.context.SaveChangesAsync();

            return smsAuth.Token;
        }
    }
}
