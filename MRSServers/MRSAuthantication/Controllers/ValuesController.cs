using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MRS.Data.Models;

namespace MRSAuthantication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;


        public RegisterController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<string>> Post(Value value)
        {
            if (value == null || !this.ModelState.IsValid)
            {
                return this.BadRequest("somethingBadHappend");
            }

            var user = new ApplicationUser { Email = value.Username, UserName = value.Username };
            var result = await this.userManager.CreateAsync(user, value.Password);

            if (result.Succeeded)
            {
                this.Ok();
            }

            return this.BadRequest("somethingBadHappend");
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
