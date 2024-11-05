using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TopCVSystemAPIdotnet.Data;
using TopCVSystemAPIdotnet.Data.Entities;

namespace API_Mobile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicantController : ControllerBase
    {
        private readonly TopCvDbContext _context;

        public ApplicantController(TopCvDbContext context)
        {
            _context = context;
        }

        // Lấy tất cả Applicant
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var applicants = await _context.Applicant.ToListAsync();
            return Ok(applicants);
        }

        // Lấy Applicant theo ID
        [HttpGet("{ID}")]
        public async Task<IActionResult> GetByID(int ID)
        {
            try
            {
                var applicant = await _context.Applicant.FindAsync(ID);
                if (applicant == null)
                {
                    return NotFound();
                }
                return Ok(applicant);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        // Lấy Applicant theo ID_User
        [HttpGet("user/{ID_User}")]
        public async Task<IActionResult> GetByUserID(int ID_User)
        {
            try
            {
                var applicant = await _context.Applicant.FirstOrDefaultAsync(a => a.ID_User == ID_User);
                if (applicant == null)
                {
                    return NotFound();
                }
                return Ok(applicant);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        // Tạo mới Applicant
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Applicant applicant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Applicant.Add(applicant);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetByID), new { ID = applicant.ID }, applicant);
        }

        // Cập nhật Applicant
        [HttpPut("{ID}")]
        public async Task<IActionResult> Edit(int ID, [FromBody] Applicant updatedApplicant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var applicant = await _context.Applicant.FindAsync(ID);
            if (applicant == null)
            {
                return NotFound();
            }

            // Update applicant information
            applicant.Applicant_Name = updatedApplicant.Applicant_Name;
            applicant.Phone_Number = updatedApplicant.Phone_Number;
            applicant.Email = updatedApplicant.Email;
            applicant.Job_Desire = updatedApplicant.Job_Desire;
            applicant.Working_Location_Desire = updatedApplicant.Working_Location_Desire;
            applicant.Working_Experience = updatedApplicant.Working_Experience;
            applicant.ID_User = updatedApplicant.ID_User;

            _context.Applicant.Update(applicant);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PUT method chỉ sửa ID_User của Applicant
        [HttpPut("user/{ID}")]
        public async Task<IActionResult> UpdateUserID(int ID, [FromBody] int newID_User)
        {
            var applicant = await _context.Applicant.FindAsync(ID);
            if (applicant == null)
            {
                return NotFound();
            }

            // Update only the ID_User field
            applicant.ID_User = newID_User;

            _context.Applicant.Update(applicant);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Xóa Applicant theo ID
        [HttpDelete("{ID}")]
        public async Task<IActionResult> Remove(int ID)
        {
            try
            {
                var applicant = await _context.Applicant.FindAsync(ID);
                if (applicant == null)
                {
                    return NotFound();
                }

                _context.Applicant.Remove(applicant);
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
