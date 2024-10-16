using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TopCVSystemAPIdotnet.Data;
using TopCVSystemAPIdotnet.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TopCVSystemAPIdotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageNotificationController : ControllerBase
    {
        private readonly TopCvDbContext _context;

        public MessageNotificationController(TopCvDbContext context)
        {
            _context = context;
        }

        // GET: api/MessageNotification
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageNotification>>> GetMessageNotifications()
        {
            return await _context.MessageNotification.ToListAsync();
        }

        // GET: api/MessageNotification/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MessageNotification>> GetMessageNotification(int id)
        {
            var messageNotification = await _context.MessageNotification.FindAsync(id);

            if (messageNotification == null)
            {
                return NotFound();
            }

            return messageNotification;
        }

        // POST: api/MessageNotification
        [HttpPost]
        public async Task<ActionResult<MessageNotification>> PostMessageNotification(MessageNotification messageNotification)
        {
            try
            {
                // Check if the Message with the specified ID exists
                var existingMessage = await _context.Message.FindAsync(messageNotification.ID_Message);
                if (existingMessage == null)
                {
                    return BadRequest($"Message with ID {messageNotification.ID_Message} does not exist.");
                }

                // Add the new MessageNotification
                _context.MessageNotification.Add(messageNotification);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetMessageNotification), new { id = messageNotification.ID }, messageNotification);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        // DELETE: api/MessageNotification/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessageNotification(int id)
        {
            var messageNotification = await _context.MessageNotification.FindAsync(id);
            if (messageNotification == null)
            {
                return NotFound();
            }

            _context.MessageNotification.Remove(messageNotification);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MessageNotificationExists(int id)
        {
            return _context.MessageNotification.Any(e => e.ID == id);
        }
    }
}
