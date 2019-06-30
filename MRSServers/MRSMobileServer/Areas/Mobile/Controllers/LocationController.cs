using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MRS.Services.Contracts;
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
        private readonly ILocationService locationService;
        private readonly IUserService userService;

        public LocationController(ILocationService locationService, IUserService userService)
        {
            this.locationService = locationService;
            this.userService = userService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult AddLocation([FromBody]CreateLocationBindingModel locationInfo)
        {
            if (locationInfo == null && !ModelState.IsValid)
            {
                return BadRequest("Location is invalid");
            }

            var location = Mapper.Map<MrsLocation>(locationInfo);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            location.UserId = userId;

            locationService.AddLocation(location);

            return StatusCode(201);
        }

        public ActionResult AddLocationWithMessage([FromBody]CreateLocationBindingModel locationInfo)
        {
            if (locationInfo == null && !ModelState.IsValid)
            {
                return BadRequest("Location is invalid");
            }

            var location = Mapper.Map<MrsLocation>(locationInfo);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            location.UserId = userId;
            location.Message.LocationId = location.Id;

            locationService.AddLocation(location);
            userService.ChangeUserCondition(userId, true);

            return StatusCode(201);
        }
    }
}