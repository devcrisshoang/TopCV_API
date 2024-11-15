using Microsoft.AspNetCore.Mvc;
using TopCVSystemAPIdotnet.Data;
using TopCVSystemAPIdotnet.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace TopCVSystemAPIdotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly TopCvDbContext _context;

        public UserController(TopCvDbContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.User.ToListAsync();
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }


        // POST: api/User
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            var existingUser = await _context.User.FirstOrDefaultAsync(u => u.Username == user.Username);
            if (existingUser != null)
            {
                return BadRequest("Username already exists.");
            }

            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.ID }, user);
        }


        // Phương thức mới để lấy tất cả tên đăng nhập của người dùng
        // GET: api/User/usernames
        [HttpGet("usernames")]
        public async Task<ActionResult<IEnumerable<string>>> GetUsernames()
        {
            var usernames = await _context.User
                                    .Select(u => u.Username)
                                    .ToListAsync();

            return Ok(usernames);
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.ID == id);
        }

        // Cập nhật Company
        [HttpPut("{ID}")]
        public async Task<IActionResult> Edit(int ID, [FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 nếu dữ liệu không hợp lệ
            }

            var people = await _context.User.FindAsync(ID);
            if (people == null)
            {
                return NotFound(); // 404 nếu không tìm thấy
            }

            people.Avatar = user.Avatar;
            people.Image_Background = user.Image_Background;
            people.Username = user.Username;
            people.Password = user.Password;
            people.UID = user.UID;

            _context.User.Update(people);
            await _context.SaveChangesAsync();

            return NoContent(); // Trả về 204 No Content sau khi cập nhật thành công
        }
    }
}
