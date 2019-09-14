using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MRS.Models.MRSSpaModels.BindingModels.MissionLog;
using MRS.Models.MRSSpaModels.ViewModels.MissionLog;
using MRS.Services.Spa.Data;
using MRS.Services.Spa.Data.Contracts;

namespace MRS.Spa.Web.Controllers
{
    [Authorize]
    public class MissionLogsController : BaseController
    {
        private readonly IMissionLogService missionLogService;

        public MissionLogsController(IMissionLogService missionLogService)
        {
            this.missionLogService = missionLogService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> AddMissionLog([FromBody]MissionLogCreateBindingModel missionLogCreateBindingModel)
        {
            if (missionLogCreateBindingModel == null && !ModelState.IsValid)
            {
                return BadRequest("Mission log is invalid");
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            missionLogCreateBindingModel.UserId = userId;

            await this.missionLogService.AddMissionLogAsync(missionLogCreateBindingModel);

            return StatusCode(201);
        }

        public async Task<ActionResult<IEnumerable<MissionLogViewModel>>> GetAllMissionLogsByUser()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var missionLogs = (await this.missionLogService.GetAllMissionLogsByUserIdAsync<MissionLogViewModel>(userId)).ToList();

            if(missionLogs == null)
            {
                return NotFound();
            }

            return Ok(missionLogs);
        }
    }
}