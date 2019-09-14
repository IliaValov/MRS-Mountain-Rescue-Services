using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MRS.Models.MRSMobileModels.ViewModels.Account;
using MRS.Services.Mobile.Data.Contracts;

namespace MRS.Mobile.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserViewModel>>> GetAllUsers ()
        {
            return (await userService.GetAllAsync<UserViewModel>()).ToList();
        }
    }
}