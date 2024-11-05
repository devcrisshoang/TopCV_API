using Microsoft.AspNetCore.Mvc;
using TopCVSystemAPIdotnet.Data;
using TopCVSystemAPIdotnet.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace TopCVSystemAPIdotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly TopCvDbContext _context;

        public MessageController(TopCvDbContext context)
        {
            _context = context;
        }

        // GET: api/Message
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessages()
        {
            return await _context.Message.ToListAsync();
        }

        // GET: api/Message/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> GetMessage(int id)
        {
            var message = await _context.Message.FindAsync(id);

            if (message == null)
            {
                return NotFound();
            }

            return message;
        }

        // GET: api/Message/GetChatPartners/{id}
        [HttpGet("GetChatPartners/{id}")]
        public async Task<ActionResult<IEnumerable<User>>> GetChatPartners(int id)
        {
            // Lấy tất cả các tin nhắn mà người dùng có ID là sender hoặc receiver
            var messages = await _context.Message
                .Where(m => m.Sender_ID == id || m.Receiver_ID == id)
                .ToListAsync();

            if (messages == null || !messages.Any())
            {
                return NotFound("No messages found for this user.");
            }

            // Lấy danh sách các đối tác nhắn tin (người không phải là chính người dùng đó)
            var chatPartnerIds = messages
                .Select(m => m.Sender_ID == id ? m.Receiver_ID : m.Sender_ID)
                .Distinct()
                .ToList();

            // Lấy thông tin chi tiết của những người dùng này từ bảng User
            var chatPartners = await _context.User
                .Where(u => chatPartnerIds.Contains(u.ID))
                .ToListAsync();

            return Ok(chatPartners);
        }

        // GET: api/Message/GetMessagesBetweenUsers/9/otherUserId
        [HttpGet("GetMessagesBetweenUsers/{userId1}/{userId2}")]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessagesBetweenUsers(int userId1, int userId2)
        {
            // Lấy tất cả các tin nhắn mà userId1 là người gửi hoặc người nhận và userId2 là đối tác
            var messages = await _context.Message
                .Where(m => (m.Sender_ID == userId1 && m.Receiver_ID == userId2) ||
                            (m.Sender_ID == userId2 && m.Receiver_ID == userId1))
                .ToListAsync();

            if (messages == null || !messages.Any())
            {
                return NotFound("No messages found between these users.");
            }

            return Ok(messages);
        }



        // POST: api/Message
        [HttpPost]
        public async Task<ActionResult<Message>> PostMessage(Message message)
        {
            // Kiểm tra nếu Sender_ID và Receiver_ID tồn tại trong bảng User
            var senderExists = await _context.User.AnyAsync(u => u.ID == message.Sender_ID);
            var receiverExists = await _context.User.AnyAsync(u => u.ID == message.Receiver_ID);

            if (!senderExists || !receiverExists)
            {
                return BadRequest("Sender or Receiver does not exist.");
            }

            // Nếu cả hai tồn tại, thêm tin nhắn vào bảng Message
            _context.Message.Add(message);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMessage), new { id = message.ID }, message);
        }

        // PUT: api/Message/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMessage(int id, Message message)
        {
            if (id != message.ID)
            {
                return BadRequest();
            }

            // Kiểm tra nếu Sender_ID và Receiver_ID tồn tại trong bảng User
            var senderExists = await _context.User.AnyAsync(u => u.ID == message.Sender_ID);
            var receiverExists = await _context.User.AnyAsync(u => u.ID == message.Receiver_ID);

            if (!senderExists || !receiverExists)
            {
                return BadRequest("Sender or Receiver does not exist.");
            }

            _context.Entry(message).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageExists(id))
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

        // DELETE: api/Message/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            var message = await _context.Message.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }

            _context.Message.Remove(message);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Message
        [HttpDelete]
        public async Task<IActionResult> DeleteAllMessages()
        {
            // Xóa tất cả tin nhắn
            _context.Message.RemoveRange(_context.Message);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MessageExists(int id)
        {
            return _context.Message.Any(e => e.ID == id);
        }
    }
}
