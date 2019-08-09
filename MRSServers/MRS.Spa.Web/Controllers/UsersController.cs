using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MRS.Models.MRSMobileModels.ViewModels.Location;

namespace MRS.Spa.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> AddLocation([FromBody]LocationViewModel locationInfo)
        {
            if (locationInfo == null && !ModelState.IsValid)
            {
                return BadRequest("Location is invalid");
            }


            return StatusCode(201);
        }
    }
}