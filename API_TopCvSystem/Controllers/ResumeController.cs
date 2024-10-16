using API_TopCvSystem.DTO;
using API_TopCvSystem.Mapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TopCVSystemAPIdotnet.Data;
using TopCVSystemAPIdotnet.Data.Entities;
using TopCVSystemAPIdotnet.DTOs;
using TopCVSystemAPIdotnet.Mappers;
using static System.Net.Mime.MediaTypeNames;

namespace API_Mobile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResumeController : ControllerBase
    {
        private readonly TopCvDbContext _context;

        public ResumeController(TopCvDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCompany()
        {
            try
            {
                // Lấy tất cả các Company từ database
                var resume = await _context.Resume.ToListAsync();

                // Kiểm tra nếu danh sách rỗng
                if (resume == null || resume.Count == 0)
                {
                    return NotFound("No resume found."); // Trả về 404 nếu không có công ty nào
                }

                // Trả về danh sách các Company
                return Ok(resume);
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ và trả về mã lỗi 500
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetResumeBy/{applicantID}")]
        public async Task<IActionResult> GetResumeByApplicantID(int applicantID)
        {
            try
            {
                // Tìm danh sách Resume dựa trên ApplicantID
                var resumes = await _context.Resume
                    .Where(r => r.ID_Applicant == applicantID)
                    .ToListAsync();

                // Kiểm tra nếu không tìm thấy bất kỳ Resume nào
                if (resumes == null || !resumes.Any())
                {
                    return NotFound($"No resumes found for Applicant with ID {applicantID}.");
                }

                // Trả về danh sách Resume
                return Ok(resumes);
            }
            catch (Exception ex)
            {
                // Trả về mã lỗi 500 nếu có lỗi
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [HttpGet("{ID}")]
        public async Task<IActionResult> GetByID(int ID)
        {
            try
            {
                var resume = await _context.Resume.FindAsync(ID);
                if (resume == null)
                {
                    return NotFound(); // 404 nếu không tìm thấy
                }

                return Ok(resume); // Trả về dữ liệu công ty
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPost("{applicantID}")]
        public async Task<IActionResult> CreateResumeForApplicant(int applicantID, [FromBody] ResumeDto resumeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 if the data is invalid
            }

            try
            {
                // Create a new resume entity based on the input resumeDto
                var newResume = new Resume
                {
                    Applicant_Name = resumeDto.Applicant_Name,
                    Email = resumeDto.Email,
                    Phone_Number = resumeDto.Phone_Number,
                    Education = resumeDto.Education,
                    Skills = resumeDto.Skills,
                    Certificate = resumeDto.Certificate,
                    Job_Applying = resumeDto.Job_Applying,
                    Introduction = resumeDto.Introduction,
                    Image = resumeDto.Image,
                    ID_Applicant = applicantID // Assign the passed ApplicantID
                };

                // Add the new resume to the database
                _context.Resume.Add(newResume);
                await _context.SaveChangesAsync();  // Save to get the generated ID

                // Return the created resume with the generated ID
                return CreatedAtAction(nameof(GetByID), new { ID = newResume.ID }, newResume);
            }
            catch (Exception ex)
            {
                // Handle the exception and return a 500 error
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        // Cập nhật Company
        [HttpPut("{ID}")]
        public async Task<IActionResult> Edit(int ID, [FromBody] ResumeDto resumeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 nếu dữ liệu không hợp lệ
            }

            var resume = await _context.Resume.FindAsync(ID);
            if (resume == null)
            {
                return NotFound(); // 404 nếu không tìm thấy
            }

            resume.Applicant_Name = resumeDto.Applicant_Name;
            resume.Email = resumeDto.Email;
            resume.Phone_Number = resumeDto.Phone_Number;
            resume.Education = resumeDto.Education;
            resume.Skills = resumeDto.Skills;
            resume.Certificate = resumeDto.Certificate;
            resume.Job_Applying = resumeDto.Job_Applying;
            resume.Introduction = resumeDto.Introduction;
            resume.Image = resumeDto.Image;

            _context.Resume.Update(resume);
            await _context.SaveChangesAsync();

            return NoContent(); // Trả về 204 No Content sau khi cập nhật thành công
        }

        // Xóa Company theo ID
        [HttpDelete("{ID}")]
        public async Task<IActionResult> Remove(int ID)
        {
            try
            {
                var resume = await _context.Resume.FindAsync(ID);
                if (resume == null)
                {
                    return NotFound(); // 404 nếu không tìm thấy
                }

                _context.Resume.Remove(resume);
                await _context.SaveChangesAsync();
                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
