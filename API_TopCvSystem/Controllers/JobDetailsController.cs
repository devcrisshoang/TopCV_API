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
    public class JobDetailsController : ControllerBase
    {
        private readonly TopCvDbContext _context;

        public JobDetailsController(TopCvDbContext context)
        {
            _context = context;
        }

        // GET: api/JobDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobDetails>>> GetJobDetails()
        {
            return await _context.JobDetail.ToListAsync();
        }

        // GET: api/JobDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JobDetails>> GetJobDetails(int id)
        {
            var jobDetails = await _context.JobDetail.FindAsync(id);

            if (jobDetails == null)
            {
                return NotFound();
            }

            return jobDetails;
        }

        // POST: api/JobDetails
        [HttpPost]
        public async Task<ActionResult<JobDetails>> PostJobDetails(JobDetails jobDetails)
        {
            try
            {
                // Check if the Job with the specified ID exists in the Job table
                var existingJob = await _context.Job.FindAsync(jobDetails.ID_Job);

                if (existingJob == null)
                {
                    // If the Job does not exist, return a BadRequest response
                    return BadRequest($"Job with ID {jobDetails.ID_Job} does not exist.");
                }

                // Add the new JobDetails
                _context.JobDetail.Add(jobDetails);
                await _context.SaveChangesAsync();

                // Return the created response with the new job details
                return CreatedAtAction(nameof(GetJobDetails), new { id = jobDetails.ID }, jobDetails);
            }
            catch (Exception ex)
            {
                // Catch any exceptions and return a 500 Internal Server Error response with the exception message
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Cập nhật Company
        [HttpPut("{ID}")]
        public async Task<IActionResult> Edit(int ID, [FromBody] JobDetails jobDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 nếu dữ liệu không hợp lệ
            }

            var existingJobDetails = await _context.JobDetail.FindAsync(ID);
            if (existingJobDetails == null)
            {
                return NotFound(); // 404 nếu không tìm thấy
            }

            existingJobDetails.Job_Description = jobDetails.Job_Description;
            existingJobDetails.Skill_Require = jobDetails.Skill_Require;
            existingJobDetails.Benefit = jobDetails.Benefit;
            existingJobDetails.Gender_Require = jobDetails.Gender_Require;
            existingJobDetails.Working_Time = jobDetails.Working_Time;
            existingJobDetails.Working_Method = jobDetails.Working_Method;
            existingJobDetails.Working_Position = jobDetails.Working_Position;
            existingJobDetails.Number_Of_People = jobDetails.Number_Of_People;
           

            _context.JobDetail.Update(existingJobDetails);
            await _context.SaveChangesAsync();

            return NoContent(); // Trả về 204 No Content sau khi cập nhật thành công
        }

        // Cập nhật Company
        [HttpPut("people{ID}")]
        public async Task<IActionResult> EditPeople(int ID, [FromBody] JobDetails jobDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 nếu dữ liệu không hợp lệ
            }

            var existingJobDetails = await _context.JobDetail.FindAsync(ID);
            if (existingJobDetails == null)
            {
                return NotFound(); // 404 nếu không tìm thấy
            }

            existingJobDetails.Number_Of_People = jobDetails.Number_Of_People;

            _context.JobDetail.Update(existingJobDetails);
            await _context.SaveChangesAsync();

            return NoContent(); // Trả về 204 No Content sau khi cập nhật thành công
        }

        // DELETE: api/JobDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobDetails(int id)
        {
            var jobDetails = await _context.JobDetail.FindAsync(id);
            if (jobDetails == null)
            {
                return NotFound();
            }

            _context.JobDetail.Remove(jobDetails);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/JobDetails/ByJob/5
        [HttpGet("ByJob/{jobId}")]
        public async Task<ActionResult<IEnumerable<JobDetails>>> GetJobDetailsByJob(int jobId)
        {
            // Check if the Job exists
            var jobExists = await _context.Job.FindAsync(jobId);
            if (jobExists == null)
            {
                return NotFound($"Job with ID {jobId} does not exist.");
            }

            // Get all JobDetails associated with the Job ID
            var jobDetailsList = await _context.JobDetail
                .Where(jd => jd.ID_Job == jobId)
                .ToListAsync();

            return Ok(jobDetailsList);
        }

        private bool JobDetailsExists(int id)
        {
            return _context.JobDetail.Any(e => e.ID == id);
        }
    }
}
