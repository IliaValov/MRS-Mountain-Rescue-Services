using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MRS.Services.MrsMobileServices.Contracts;
using MRS.Web.Infrastructure;
using MRSMobile.Data.Models;
using MRSMobileServer.ViewModels.Account;

namespace MRSMobileServer.Controllers
{
    [AllowAnonymous]
    public class AccountController : BaseController
    {
        private readonly IOptions<SmsOptions> options;
        private readonly UserManager<MrsMobileUser> userManager;
        private readonly ISmsService smsService;
        private readonly IDeviceService deviceService;

        public AccountController(UserManager<MrsMobileUser> userManager,IOptions<SmsOptions> options ,ISmsService smsService, IDeviceService deviceService)
        {
            this.userManager = userManager;
            this.options = options;
            this.smsService = smsService;
            this.deviceService = deviceService;
        }

        [HttpPost]
        public async Task<ActionResult<string>> SmsVerification([FromBody]UserLoginBindingModel model)
        {
            if (model == null || !this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState.Values.FirstOrDefault());
            }

            var accountSid = this.options.Value.AccountSid;
            var authToken = this.options.Value.AuthToken;
            var fromNumber = this.options.Value.PhoneNumber;

            var user = await this.userManager.FindByEmailAsync(model.PhoneNumber);

            var token = await smsService.SendSms(accountSid, authToken, fromNumber, model.PhoneNumber, user.Id);

            //TODO
            if (!token.Equals(""))
            {
                return token;
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody]UserRegisterBindingModel model)
        {
            if (model == null || !this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState.Values.FirstOrDefault());
            }

            var deviceId = deviceService.AddDevice(model.Device);

            var user = new MrsMobileUser { Email = model.PhoneNumber, UserName = model.PhoneNumber, DeviceId = deviceId };
            var result = await this.userManager.CreateAsync(user, model.PhoneNumber);

            if (result.Succeeded)
            {
                return this.Ok();
            }

            return this.BadRequest(result.Errors.FirstOrDefault());
        }
    }
}