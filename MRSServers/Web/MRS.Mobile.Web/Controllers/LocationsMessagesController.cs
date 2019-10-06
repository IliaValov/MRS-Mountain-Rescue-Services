using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MRS.Common;
using MRS.Mobile.Data.Models;
using MRS.Models.MRSMobileModels.BindingModels.Location;
using MRS.Services.Mobile.Data.Contracts;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MRS.Mobile.Web.Controllers
{
    [ApiController]
    [Authorize]
    public class LocationsMessagesController : BaseController
    {
        private readonly ILocationService locationService;
        private readonly IUserService userService;

        public LocationsMessagesController(ILocationService locationService, IUserService userService)
        {
            this.locationService = locationService;
            this.userService = userService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> AddLocation([FromBody]LocationCreateBindingModel locationInfo)
        {
            if (locationInfo == null && !ModelState.IsValid)
            {
                return BadRequest(GlobalConstants.ErrorLocationIsInValid);
            }

            var location = Mapper.Map<MrsMobileLocation>(locationInfo);

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            location.UserId = userId;

            await locationService.AddLocationAsync(location);

            return StatusCode(201);
        }

        public async Task<ActionResult> AddLocationWithMessage([FromBody]LocationCreateBindingModel locationInfo)
        {
            if (locationInfo == null && !ModelState.IsValid)
            {
                return BadRequest(GlobalConstants.ErrorLocationIsInValid);
            }

            var location = Mapper.Map<MrsMobileLocation>(locationInfo);

            

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            location.UserId = userId;
            location.Message.LocationId = location.Id;
            location.Message.UserId = userId;

            await locationService.AddLocationAsync(location);
            await userService.ChangeUserConditionAsync(userId, true);

            return StatusCode(201);
        }
    }
}