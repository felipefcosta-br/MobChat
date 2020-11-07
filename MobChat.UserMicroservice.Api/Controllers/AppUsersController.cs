using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobChat.UserMicroservice.Domain.AggregatesModel.UserAggregate;
using MobChat.UserMicroservice.Infra.DataAccess;

namespace MobChat.UserMicroservice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUsersController : ControllerBase
    {
        private readonly IAppUserService appUserService;

        public AppUsersController(IAppUserService appUserService)
        {
            this.appUserService = appUserService;
        }

        // GET: api/AppUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetAppUsers()
        {
            return Ok(await appUserService.GetAllUsersAsync());
        }

        // GET: api/AppUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetAppUser(Guid id)
        {
            var appUser = await appUserService.GetUserAsync(id);

            if (appUser == null)
            {
                return NotFound();
            }

            return appUser;
        }

        // GET: api/AppUsers/account/5
        [HttpGet("account/{accountId}")]
        public async Task<ActionResult<AppUser>> GetAppUserByAccountId(Guid accountId)
        {
            var appUser = await appUserService.GetUserByAccountIdAsync(accountId);

            if (appUser == null)
            {
                return NotFound();
            }

            return appUser;
        }

        // GET: api/AppUsers/UserName/5
        [HttpGet("username/{username}")]
        public async Task<ActionResult<AppUser>> GetAppUserByUserName(String userName)
        {
            var appUser = await appUserService.GetAppUserByUserNameAsync(userName);

            if (appUser == null)
            {
                return NotFound();
            }

            return appUser;
        }

        [HttpGet("search/user/{searchTxt}")]
        public ActionResult<IEnumerable<AppUser>> SearchForUser(string searchTxt)
        {
            IEnumerable<AppUser> result = appUserService.SearchForUser(searchTxt);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // PUT: api/AppUsers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppUser(Guid id, AppUser appUser)
        {
            if (id != appUser.Id)
            {
                return BadRequest();
            }

            var result = await appUserService.UpdateUserAsync(appUser);

            if (!result)
                return NotFound();

            return NoContent();
        }

        // POST: api/AppUsers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<AppUser>> PostAppUser(AppUser appUser)
        {
            var result = await appUserService.AddUserAsync(appUser);

            if (!result)
                BadRequest("User invalid");

            return Created("api/appuser", appUser);

        }

        // DELETE: api/AppUsers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AppUser>> DeleteAppUser(Guid id)
        {
            var result = await appUserService.DeleteUserAsync(id);

            if (!result)
                return NotFound("User not found!");

            return Ok(id);
        }
        
        
    }
}
