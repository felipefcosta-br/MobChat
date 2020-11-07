using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobChat.ChatMicroservice.Domain.AggregatesModel.ConnectedUserAggregate;
using MobChat.ChatMicroservice.Infra.DataAccess;

namespace MobChat.ChatMicroservice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectedUsersController : ControllerBase
    {
        private readonly IConnectedUserService connectedUserService;

        public ConnectedUsersController(IConnectedUserService connectedUserService)
        {
            this.connectedUserService = connectedUserService;
        }

        // GET: api/ConnectedUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConnectedUser>>> GetConnectedUser()
        {
            return Ok(await connectedUserService.GetAllConnectedUsersAsync());
        }

        // GET: api/ConnectedUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ConnectedUser>> GetConnectedUser(Guid id)
        {
            //var connectedUser = await _context.ConnectedUser.FindAsync(id);

            //if (connectedUser == null)
            //{
                return NotFound();
            //}

            //return connectedUser;
        }

        // PUT: api/ConnectedUsers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConnectedUser(Guid id, ConnectedUser connectedUser)
        {
            //if (id != connectedUser.Id)
            //{
                //return BadRequest();
            //}

            //_context.Entry(connectedUser).State = EntityState.Modified;

            //try
            //{
                //await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
                //if (!ConnectedUserExists(id))
                //{
                    //return NotFound();
                //}
                //else
                //{
                    //throw;
                //}
            //}

            return NoContent();
        }

        // POST: api/ConnectedUsers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost]
        //public async Task<ActionResult<ConnectedUser>> PostConnectedUser(ConnectedUser connectedUser)
        //{
        //    await connectedUserService.AddConnectedUserAsync(connectedUser);

        //    return CreatedAtAction("GetConnectedUser", new { id = connectedUser.Id }, connectedUser);
        //}

        // DELETE: api/ConnectedUsers/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<ConnectedUser>> DeleteConnectedUser(Guid id)
        //{
        //    var connectedUser = await _context.ConnectedUser.FindAsync(id);
        //    if (connectedUser == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.ConnectedUser.Remove(connectedUser);
        //    await _context.SaveChangesAsync();

        //    return connectedUser;
        //}

        //private bool ConnectedUserExists(Guid id)
        //{
        //    return connectedUserService
        //}
    }
}
