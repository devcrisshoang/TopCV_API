using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TopCVSystemAPIdotnet.Data;
using TopCVSystemAPIdotnet.Data.Entities;
using TopCVSystemAPIdotnet.DTOs;
using TopCVSystemAPIdotnet.Mappers;

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
            var Applicant = await _context.Applicant.ToListAsync(); // Đảm bảo rằng bảng Applicant đã tồn tại
            var applicantDtos = Applicant.Select(applicant => ApplicantMapper.ToDto(applicant)).ToList();
            return Ok(applicantDtos);
        }

        // Lấy Applicant theo ID
        [HttpGet("{ID}")]
        public async Task<IActionResult> GetByID(int ID)
        {
            try
            {
                var candidate = await _context.Applicant.FindAsync(ID); // Thay đổi từ _context.Applicant thành _context.Applicant
                if (candidate == null)
                {
                    return NotFound(); // 404
                }
                var applicantDto = ApplicantMapper.ToDto(candidate);
                return Ok(applicantDto);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        // Tạo mới Applicant
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ApplicantDto applicantDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 nếu dữ liệu không hợp lệ
            }

            var newApplicant = new Applicant
            {
                Applicant_Name = applicantDto.Applicant_Name,
                Phone_Number = applicantDto.Phone_Number,
                Job_Desire = applicantDto.Job_Desire,
                Working_Location_Desire = applicantDto.Working_Location_Desire,
                Working_Experience = applicantDto.Working_Experience,
                Email = applicantDto.Email,
            };

            _context.Applicant.Add(newApplicant); 
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetByID), new { ID = newApplicant.ID }, ApplicantMapper.ToDto(newApplicant)); // Trả về 201 Created
        }

        // Cập nhật Applicant
        [HttpPut("{ID}")]
        public async Task<IActionResult> Edit(int ID, [FromBody] ApplicantDto applicantDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 nếu dữ liệu không hợp lệ
            }

            var candidate = await _context.Applicant.FindAsync(ID); // Thay đổi từ _context.Applicant thành _context.Applicant
            if (candidate == null)
            {
                return NotFound(); // 404 nếu không tìm thấy
            }

            // Cập nhật thông tin
            candidate.Applicant_Name = applicantDto.Applicant_Name;
            candidate.Phone_Number = applicantDto.Phone_Number;
            candidate.Email = applicantDto.Email;
            candidate.Job_Desire = applicantDto.Job_Desire;
            candidate.Working_Location_Desire = applicantDto.Working_Location_Desire;
            candidate.Working_Experience = applicantDto.Working_Experience;

            _context.Applicant.Update(candidate); // Thay đổi từ _context.Applicant.Update thành _context.Applicant.Update
            await _context.SaveChangesAsync();

            return NoContent(); // Trả về 204 No Content sau khi cập nhật thành công
        }

        // Xóa Applicant theo ID
        [HttpDelete("{ID}")]
        public async Task<IActionResult> Remove(int ID)
        {
            try
            {
                var candidate = await _context.Applicant.FindAsync(ID); // Thay đổi từ _context.Applicant thành _context.Applicant
                if (candidate == null)
                {
                    return NotFound(); // 404 nếu không tìm thấy
                }

                _context.Applicant.Remove(candidate); // Thay đổi từ _context.Applicant.Remove thành _context.Applicant.Remove
                await _context.SaveChangesAsync();
                return NoContent(); // 204 No Content
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
