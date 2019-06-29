using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MRSMobile.Data;
using MRSMobile.Data.Models;
using MRSMobileServer.Areas.Mobile.Views.Location;
using MRSMobileServer.Controllers;
using System.Security.Claims;

namespace MRSMobileServer.Areas.Mobile.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class LocationController : BaseController
    {
        MrsMobileContext context;

        public LocationController(MrsMobileContext context)
        {
            this.context = context;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult AddLocation([FromBody]CreateLocationBindingModel locationInfo)
        {
            if(locationInfo == null && !ModelState.IsValid)
            {
                return BadRequest("Location is invalid");
            }

            var location = Mapper.Map<MrsLocation>(locationInfo);

            location.UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            this.context.MrsLocations.Add(location);
            this.context.SaveChanges();

            return StatusCode(201);
        }

    }
}