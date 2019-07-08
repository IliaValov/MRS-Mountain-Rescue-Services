using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MRSWeb.Data.Models;
using MRSWebServer.ViewModels.account;
using System.Linq;
using System.Threading.Tasks;

namespace MRSWebServer.Controllers
{
    [AllowAnonymous]
    public class AccountController : BaseController
    {
        private readonly UserManager<MrsWebUser> userManager;

        public AccountController(UserManager<MrsWebUser> userManager)
        {
            this.userManager = userManager;
        }

        //[HttpPost]
        //public async Task<IActionResult> Login([FromBody]UserLoginBindingModel model)
        //{
        //    if (model == null || !this.ModelState.IsValid)
        //    {
        //        return this.BadRequest(this.ModelState.Values.FirstOrDefault());
        //    }


        //    return BadRequest();
        //}

        [HttpPost]
        public async Task<IActionResult> Register([FromBody]UserRegisterBindingModel model)
        {
            if (model == null || !this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState.Values.FirstOrDefault());
            }

            var user = new MrsWebUser { Email = model.Email, UserName = model.Email };
            var result = await this.userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return this.Ok();
            }

            return this.BadRequest(result.Errors.FirstOrDefault());
        }
    }
}
