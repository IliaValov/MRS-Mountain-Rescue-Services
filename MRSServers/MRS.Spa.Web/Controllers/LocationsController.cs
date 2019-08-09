using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MRS.Models.MRSMobileModels.BindingModels.Location;

namespace MRS.Spa.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> AddLocation([FromBody]LocationUserAddBindingModel locationInfo)
        {
            if (locationInfo == null && !ModelState.IsValid)
            {
                return BadRequest("Location is invalid");
            }


           
            return StatusCode(201);
        }
    }
}