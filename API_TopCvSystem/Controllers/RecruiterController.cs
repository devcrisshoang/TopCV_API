using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TopCVSystemAPIdotnet.Data;
using TopCVSystemAPIdotnet.Data.Entities;

namespace API_Mobile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecruiterController : ControllerBase
    {
        private readonly TopCvDbContext _context;

        public RecruiterController(TopCvDbContext context)
        {
            _context = context;
        }

        // Lấy tất cả Recruiter
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var recruiters = await _context.Recruiter.ToListAsync();

                if (recruiters == null || recruiters.Count == 0)
                {
                    return NotFound("No recruiters found.");
                }

                return Ok(recruiters); // Trả về danh sách trực tiếp
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Lấy Recruiter theo ID
        [HttpGet("{ID}")]
        public async Task<IActionResult> GetByID(int ID)
        {
            try
            {
                var recruiter = await _context.Recruiter.FindAsync(ID);
                if (recruiter == null)
                {
                    return NotFound();
                }

                return Ok(recruiter); // Trả về trực tiếp đối tượng Recruiter
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        // Tạo mới Recruiter
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Recruiter recruiter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newRecruiter = new Recruiter
            {
                Recruiter_Name = recruiter.Recruiter_Name,
                Phone_Number = recruiter.Phone_Number,
                Email_Address = recruiter.Email_Address,
                ID_Company = recruiter.ID_Company,
                ID_User = recruiter.ID_User,
                Front_Image = recruiter.Front_Image,
                Back_Image = recruiter.Back_Image
            };

            _context.Recruiter.Add(newRecruiter);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetByID), new { ID = newRecruiter.ID }, newRecruiter); // Trả về 201 Created với đối tượng vừa tạo
        }

        // Cập nhật Recruiter
        [HttpPut("{ID}")]
        public async Task<IActionResult> Edit(int ID, [FromBody] Recruiter recruiter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingRecruiter = await _context.Recruiter.FindAsync(ID);
            if (existingRecruiter == null)
            {
                return NotFound();
            }

            // Cập nhật các thông tin
            existingRecruiter.Recruiter_Name = recruiter.Recruiter_Name;
            existingRecruiter.Phone_Number = recruiter.Phone_Number;
            existingRecruiter.Email_Address = recruiter.Email_Address;
            existingRecruiter.ID_Company = recruiter.ID_Company;
            existingRecruiter.ID_User = recruiter.ID_User;
            existingRecruiter.Front_Image = recruiter.Front_Image;
            existingRecruiter.Back_Image = recruiter.Back_Image;

            _context.Recruiter.Update(existingRecruiter);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Xóa Recruiter theo ID
        [HttpDelete("{ID}")]
        public async Task<IActionResult> Remove(int ID)
        {
            try
            {
                var recruiter = await _context.Recruiter.FindAsync(ID);
                if (recruiter == null)
                {
                    return NotFound();
                }

                _context.Recruiter.Remove(recruiter);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
