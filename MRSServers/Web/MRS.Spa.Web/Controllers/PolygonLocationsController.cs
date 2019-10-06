using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MRS.Common;
using MRS.Models.MRSSpaModels.BindingModels.Polygon;
using MRS.Models.MRSSpaModels.ViewModels.Polygon;
using MRS.Services.Spa.Data.Contracts;

namespace MRS.Spa.Web.Controllers
{
    [Authorize]
    public class PolygonLocationsController : BaseController
    {
        private readonly IPolygonService polygonService;
        private readonly ILocationService locationService;

        public PolygonLocationsController(IPolygonService polygonService, ILocationService locationService)
        {
            this.polygonService = polygonService;
            this.locationService = locationService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> AddPolygonWithLocations([FromBody]PolygonCreateBindingModel polygonCreateBindingModel)
        {
            if (polygonCreateBindingModel == null && !ModelState.IsValid)
            {
                return BadRequest(GlobalConstants.ErrorPolygonIsInValid);
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            polygonCreateBindingModel.UserId = userId;

            await this.polygonService.AddPolygonAsync(polygonCreateBindingModel);
            
            return StatusCode(201);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PolygonViewModel>>> GetAllPolygonsByUser()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var polygons = await this.polygonService.GetPolygonsByUserIdAsync<PolygonViewModel>(userId);

            if(polygons == null)
            {
                return NotFound();
            }

            return Ok(polygons);
        }
    }
}