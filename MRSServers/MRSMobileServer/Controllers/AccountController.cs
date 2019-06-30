﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MRSMobile.Data.Models;
using MRSMobileServer.ViewModels.Account;

namespace MRSMobileServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [AllowAnonymous]
    public class AccountController : BaseController
    {
        private readonly UserManager<MrsUser> userManager;

        public AccountController(UserManager<MrsUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody]UserRegisterBindingModel model)
        {
            if (model == null || !this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState.Values.FirstOrDefault());
            }

            var user = new MrsUser { UserName = model.PhoneNumber };
            var result = await this.userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return this.Ok();
            }

            return this.BadRequest(result.Errors.FirstOrDefault());
        }
    }
}