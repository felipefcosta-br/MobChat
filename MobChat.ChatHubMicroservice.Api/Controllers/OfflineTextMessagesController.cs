using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobChat.ChatHubMicroservice.Domain.AggregatesModel.OfflineTextMessageAggregate;
using MobChat.ChatHubMicroservice.Infra.DataAccess;

namespace MobChat.ChatHubMicroservice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfflineTextMessagesController : ControllerBase
    {
        private readonly ChatHubContext _context;

        public OfflineTextMessagesController(ChatHubContext context)
        {
            _context = context;
        }

        // GET: api/OfflineTextMessages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OfflineTextMessage>>> GetOfflineTextMessage()
        {
            return await _context.OfflineTextMessage.ToListAsync();
        }

        // GET: api/OfflineTextMessages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OfflineTextMessage>> GetOfflineTextMessage(Guid id)
        {
            var offlineTextMessage = await _context.OfflineTextMessage.FindAsync(id);

            if (offlineTextMessage == null)
            {
                return NotFound();
            }

            return offlineTextMessage;
        }

        // PUT: api/OfflineTextMessages/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOfflineTextMessage(Guid id, OfflineTextMessage offlineTextMessage)
        {
            if (id != offlineTextMessage.Id)
            {
                return BadRequest();
            }

            _context.Entry(offlineTextMessage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OfflineTextMessageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/OfflineTextMessages
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<OfflineTextMessage>> PostOfflineTextMessage(OfflineTextMessage offlineTextMessage)
        {
            _context.OfflineTextMessage.Add(offlineTextMessage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOfflineTextMessage", new { id = offlineTextMessage.Id }, offlineTextMessage);
        }

        // DELETE: api/OfflineTextMessages/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<OfflineTextMessage>> DeleteOfflineTextMessage(Guid id)
        {
            var offlineTextMessage = await _context.OfflineTextMessage.FindAsync(id);
            if (offlineTextMessage == null)
            {
                return NotFound();
            }

            _context.OfflineTextMessage.Remove(offlineTextMessage);
            await _context.SaveChangesAsync();

            return offlineTextMessage;
        }

        private bool OfflineTextMessageExists(Guid id)
        {
            return _context.OfflineTextMessage.Any(e => e.Id == id);
        }
    }
}
