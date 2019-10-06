using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MRS.Common;
using MRS.Models.MRSMobileModels.ViewModels.Account;

namespace MRS.Spa.Web.Controllers
{

    public class UsersController : BaseController
    {

        //TODO Change the view model
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> AddUser([FromBody]UserViewModel userInfo)
        {
            if (userInfo == null && !ModelState.IsValid)
            {
                return BadRequest(GlobalConstants.ErrorLocationIsInValid);
            }


            return StatusCode(201);
        }
    }
}