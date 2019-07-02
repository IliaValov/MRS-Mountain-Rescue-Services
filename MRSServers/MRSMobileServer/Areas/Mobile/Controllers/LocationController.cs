using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MRS.Services.Contracts;
using MRSMobile.Data.Models;
using MRSMobileServer.Areas.Mobile.Views.Location;
using MRSMobileServer.Controllers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MRSMobileServer.Areas.Mobile.Controllers
{
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
        public async Task<ActionResult> AddLocation([FromBody]CreateLocationBindingModel locationInfo)
        {
            if (locationInfo == null && !ModelState.IsValid)
            {
                return BadRequest("Location is invalid");
            }

            var location = Mapper.Map<MrsLocation>(locationInfo);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            location.UserId = userId;

            await locationService.AddLocation(location);

            return StatusCode(201);
        }

        public async Task<ActionResult> AddLocationWithMessage([FromBody]CreateLocationBindingModel locationInfo)
        {
            if (locationInfo == null && !ModelState.IsValid)
            {
                return BadRequest("Location is invalid");
            }

            var location = Mapper.Map<MrsLocation>(locationInfo);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            location.UserId = userId;
            location.Message.LocationId = location.Id;

            await locationService.AddLocation(location);
            userService.ChangeUserCondition(userId, true);

            return StatusCode(201);
        }
    }
}