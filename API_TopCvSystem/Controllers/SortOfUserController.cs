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
    public class SortOfUserController : ControllerBase
    {
        private readonly TopCvDbContext _context;

        public SortOfUserController(TopCvDbContext context)
        {
            _context = context;
        }

        // GET: api/SortOfUser
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SortOfUser>>> GetSortOfUser()
        {
            return await _context.SortOfUser.ToListAsync();
        }

        // GET: api/SortOfUser/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SortOfUser>> GetSortOfUser(int id)
        {
            var sortOfUser = await _context.SortOfUser.FindAsync(id);

            if (sortOfUser == null)
            {
                return NotFound();
            }

            return sortOfUser;
        }

        // POST: api/SortOfUser
        [HttpPost]
        public async Task<ActionResult<SortOfUser>> PostSortOfUser(SortOfUser sortOfUser)
        {
            try
            {
                // Check if the specified ID_User exists in the User table (if applicable)
                var userExists = await _context.User.FindAsync(sortOfUser.ID_User); // Assuming you have a User table
                if (userExists == null)
                {
                    return BadRequest($"User with ID {sortOfUser.ID_User} does not exist.");
                }

                _context.SortOfUser.Add(sortOfUser);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetSortOfUser), new { id = sortOfUser.ID_SortOfUser }, sortOfUser);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        // Cập nhật Company
        [HttpPut("{ID}Applicant")]
        public async Task<IActionResult> EditApplicant(int ID, [FromBody] SortOfUser sortOfUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 nếu dữ liệu không hợp lệ
            }

            var sort = await _context.SortOfUser.FindAsync(ID);
            if (sort == null)
            {
                return NotFound(); // 404 nếu không tìm thấy
            }

            sort.Applicant = sortOfUser.Applicant;
            
            _context.SortOfUser.Update(sort);
            await _context.SaveChangesAsync();

            return NoContent(); // Trả về 204 No Content sau khi cập nhật thành công
        }
        // Cập nhật Company
        [HttpPut("{ID}Recruiter")]
        public async Task<IActionResult> EditRecruiter(int ID, [FromBody] SortOfUser sortOfUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 nếu dữ liệu không hợp lệ
            }

            var sort = await _context.SortOfUser.FindAsync(ID);
            if (sort == null)
            {
                return NotFound(); // 404 nếu không tìm thấy
            }

            
            sort.Recruiter = sortOfUser.Recruiter;

            _context.SortOfUser.Update(sort);
            await _context.SaveChangesAsync();

            return NoContent(); // Trả về 204 No Content sau khi cập nhật thành công
        }


        // DELETE: api/SortOfUser/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSortOfUser(int id)
        {
            var sortOfUser = await _context.SortOfUser.FindAsync(id);
            if (sortOfUser == null)
            {
                return NotFound();
            }

            _context.SortOfUser.Remove(sortOfUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SortOfUserExists(int id)
        {
            return _context.SortOfUser.Any(e => e.ID_SortOfUser == id);
        }
    }
}
