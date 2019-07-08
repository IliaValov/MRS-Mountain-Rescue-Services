using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MRS.Services.MrsMobileServices.Contracts;
using MRS.Web.Infrastructure;
using MRS.Web.Infrastructure.Middlewares.Auth;
using MRSMobile.Data.Models;
using MRSMobileServer.ViewModels.Account;

namespace MRSMobileServer.Controllers
{
    [AllowAnonymous]
    public class AccountController : BaseController
    {
        private readonly UserManager<MrsMobileUser> userManager;
        private readonly IOptions<SmsOptions> options;
        private readonly ISmsService smsService;

        public AccountController(UserManager<MrsMobileUser> userManager, ISmsService smsService)
        {
            this.smsService = smsService;
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


            var token = smsService.SendSms(accountSid, authToken, fromNumber, model.PhoneNumber, "");

            //TODO
            if (token.Equals(""))
            {
                return "token";
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

            var user = new MrsMobileUser { Email = model.Email, UserName = model.PhoneNumber };
            var result = await this.userManager.CreateAsync(user, model.PhoneNumber);

            if (result.Succeeded)
            {
                return this.Ok();
            }

            return this.BadRequest(result.Errors.FirstOrDefault());
        }
    }
}