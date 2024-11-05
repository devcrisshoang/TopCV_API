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
    public class NotificationController : ControllerBase
    {
        private readonly TopCvDbContext _context;

        public NotificationController(TopCvDbContext context)
        {
            _context = context;
        }

        // GET: api/Notification
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notification>>> GetNotification()
        {
            return await _context.Notification.ToListAsync();
        }

        // GET: api/Notification/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Notification>> GetNotification(int id)
        {
            var notification = await _context.Notification.FindAsync(id);

            if (notification == null)
            {
                return NotFound();
            }

            return notification;
        }

        // GET: api/Notification/User/5
        [HttpGet("User/{userId}")]
        public async Task<ActionResult<IEnumerable<Notification>>> GetNotificationByUserId(int userId)
        {
            // Kiểm tra xem người dùng với ID đã tồn tại không
            var existingUser = await _context.User.FindAsync(userId);
            if (existingUser == null)
            {
                return NotFound($"User with ID {userId} does not exist.");
            }

            // Truy vấn để lấy danh sách thông báo của người dùng
            var notifications = await _context.Notification
                .Where(n => n.ID_User == userId)
                .ToListAsync();

            if (notifications == null || !notifications.Any())
            {
                return NotFound($"No notifications found for User with ID {userId}.");
            }

            return Ok(notifications);
        }



        // POST: api/Notification
        [HttpPost]
        public async Task<ActionResult<Notification>> PostNotification(Notification notification)
        {
            try
            {
                // Check if the User with the specified ID exists
                var existingUser = await _context.User.FindAsync(notification.ID_User);
                if (existingUser == null)
                {
                    return BadRequest($"User with ID {notification.ID_User} does not exist.");
                }

                // Add the new Notification
                _context.Notification.Add(notification);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetNotification), new { id = notification.ID }, notification);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Cập nhật Company
        [HttpPut("{ID}")]
        public async Task<IActionResult> Edit(int ID, [FromBody] Notification notification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 nếu dữ liệu không hợp lệ
            }

            var existingNotification = await _context.Notification.FindAsync(ID);
            if (existingNotification == null)
            {
                return NotFound(); // 404 nếu không tìm thấy
            }

            existingNotification.Content = notification.Content;


            _context.Notification.Update(existingNotification);
            await _context.SaveChangesAsync();

            return NoContent(); // Trả về 204 No Content sau khi cập nhật thành công
        }


        // DELETE: api/Notification/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            var notification = await _context.Notification.FindAsync(id);
            if (notification == null)
            {
                return NotFound();
            }

            _context.Notification.Remove(notification);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NotificationExists(int id)
        {
            return _context.Notification.Any(e => e.ID == id);
        }
    }
}
