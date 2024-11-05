using API_TopCvSystem.DTO;
using API_TopCvSystem.Mapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TopCVSystemAPIdotnet.Data;
using TopCVSystemAPIdotnet.Data.Entities;
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
        public async Task<IActionResult> GetAllResumes()
        {
            try
            {
                var resumes = await _context.Resume.ToListAsync();
                if (resumes == null || resumes.Count == 0)
                {
                    return NotFound("No resumes found.");
                }

                return Ok(resumes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetResumeBy/{applicantID}")]
        public async Task<IActionResult> GetResumeByApplicantID(int applicantID)
        {
            try
            {
                var resumes = await _context.Resume
                    .Where(r => r.ID_Applicant == applicantID)
                    .ToListAsync();

                if (resumes == null || !resumes.Any())
                {
                    return NotFound($"No resumes found for Applicant with ID {applicantID}.");
                }

                return Ok(resumes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetResumeIdBy/{ID}")]
        public async Task<IActionResult> GetByID(int ID)
        {
            try
            {
                var resume = await _context.Resume.FindAsync(ID);
                if (resume == null)
                {
                    return NotFound();
                }

                return Ok(resume);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateResume([FromBody] Resume resume)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Kiểm tra nếu ID_Applicant tồn tại trong bảng Applicant
                var applicant = await _context.Applicant.FindAsync(resume.ID_Applicant);
                if (applicant == null)
                {
                    return BadRequest($"Applicant with ID {resume.ID_Applicant} does not exist.");
                }

                // Thêm resume mới vào database
                _context.Resume.Add(resume);
                await _context.SaveChangesAsync(); 

                return CreatedAtAction(nameof(GetByID), new { ID = resume.ID }, resume);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{ID}")]
        public async Task<IActionResult> Edit(int ID, [FromBody] Resume resume)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingResume = await _context.Resume.FindAsync(ID);
            if (existingResume == null)
            {
                return NotFound();
            }

            // Cập nhật thông tin resume
            existingResume.Applicant_Name = resume.Applicant_Name;
            existingResume.Email = resume.Email;
            existingResume.Phone_Number = resume.Phone_Number;
            existingResume.Education = resume.Education;
            existingResume.Skills = resume.Skills;
            existingResume.Certificate = resume.Certificate;
            existingResume.Job_Applying = resume.Job_Applying;
            existingResume.Introduction = resume.Introduction;
            existingResume.Image = resume.Image;
            existingResume.Experience = resume.Experience;

            _context.Resume.Update(existingResume);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{ID}")]
        public async Task<IActionResult> Remove(int ID)
        {
            try
            {
                var resume = await _context.Resume.FindAsync(ID);
                if (resume == null)
                {
                    return NotFound();
                }

                _context.Resume.Remove(resume);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpDelete("DeleteAll")]
        public async Task<IActionResult> DeleteAllResumes()
        {
            try
            {
                // Lấy tất cả Resume
                var resumes = await _context.Resume.ToListAsync();

                if (resumes == null || !resumes.Any())
                {
                    return NotFound("No resumes found to delete.");
                }

                // Xóa tất cả Resume
                _context.Resume.RemoveRange(resumes);
                await _context.SaveChangesAsync();

                return NoContent(); // Trả về HTTP 204 No Content
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    }
}
