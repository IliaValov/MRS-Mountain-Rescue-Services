using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MRS.Mobile.Data.Models;
using MRS.Models.MRSMobileModels.BindingModels.Account;
using MRS.Services.Contracts;
using MRS.Services.Mobile.Data.Contracts;
using MRS.Web.Infrastructure;

namespace MRS.Mobile.Web.Controllers
{
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IOptions<SmsOptions> options;
        private readonly UserManager<MrsMobileUser> userManager;
        private readonly ISmsService smsService;
        private readonly IDeviceService deviceService;
        private readonly ISmsAuthanticationService smsAuthanticationService;

        public AccountController(UserManager<MrsMobileUser> userManager, IOptions<SmsOptions> options, ISmsService smsService, IDeviceService deviceService, ISmsAuthanticationService smsAuthanticationService)
        {
            this.userManager = userManager;
            this.options = options;
            this.smsService = smsService;
            this.deviceService = deviceService;
            this.smsAuthanticationService = smsAuthanticationService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Authanticate()
        {
            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<string>> SmsVerification([FromBody]UserLoginBindingModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.FirstOrDefault());
            }

            var accountSid = options.Value.AccountSid;
            var authToken = options.Value.AuthToken;
            var fromNumber = options.Value.PhoneNumber;

            var user = await userManager.FindByEmailAsync(model.PhoneNumber);

            if (user == null)
            {
                return BadRequest();
            }

            var verificationCode = await smsService.SendSms(accountSid, authToken, fromNumber, model.PhoneNumber);

            //TODO
            if (!verificationCode.Equals(""))
            {
                return verificationCode;
            }

            return BadRequest();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Register([FromBody]UserRegisterBindingModel model)
        {
            if (model == null || !ModelState.IsValid || model.PhoneNumber.Length < 5)
            {
                return BadRequest(ModelState.Values.FirstOrDefault());
            }

            var accountSid = options.Value.AccountSid;
            var authToken = options.Value.AuthToken;
            var fromNumber = options.Value.PhoneNumber;

            var verificationCode = await smsService.SendSms(accountSid, authToken, fromNumber, model.PhoneNumber);

            if (string.IsNullOrEmpty(verificationCode))
            {
                return BadRequest();
            }

            if (await userManager.FindByNameAsync(model.PhoneNumber) == null)
            {
                var deviceId = await deviceService.AddDeviceAsync(model.Device);

                var user = new MrsMobileUser { Email = model.PhoneNumber, UserName = model.PhoneNumber, PhoneNumber = model.PhoneNumber, DeviceId = deviceId };
                var result = await userManager.CreateAsync(user, model.PhoneNumber);


                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors.FirstOrDefault());
                }

                var addRole = await userManager.AddToRoleAsync(user, "User");

                if (!addRole.Succeeded)
                {
                    return BadRequest(addRole.Errors.FirstOrDefault());
                }
            }

            var token = await smsAuthanticationService.AddSmsTokenAsync(userManager.FindByNameAsync(model.PhoneNumber).Result.Id, verificationCode);

            if (!string.IsNullOrEmpty(token))
            {
                return token;
            }

            return BadRequest();
        }
    }
}