using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TopCVSystemAPIdotnet.Data;
using TopCVSystemAPIdotnet.Data.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace API_TopCvSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly TopCvDbContext _context;

        public JobController(TopCvDbContext context)
        {
            _context = context;
        }

        // GET: api/Job
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job>>> GetJob()
        {
            return await _context.Job.ToListAsync();
        }

        // GET: api/Job/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Job>> GetJob(int id)
        {
            var job = await _context.Job.FindAsync(id);

            if (job == null)
            {
                return NotFound();
            }

            return job;
        }

        // POST: api/Job
        [HttpPost]
        public async Task<ActionResult<Job>> PostJob(Job job)
        {
            try
            {
                // Check if the recruiter exists
                var recruiterExists = await _context.Recruiter.FindAsync(job.ID_Recruiter);
                if (recruiterExists == null)
                {
                    return BadRequest($"Recruiter with ID {job.ID_Recruiter} does not exist.");
                }

                // Add the new Job
                _context.Job.Add(job);
                await _context.SaveChangesAsync();

                // Return the newly created job
                return CreatedAtAction(nameof(GetJob), new { id = job.ID }, job);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Cập nhật Company
        [HttpPut("{ID}")]
        public async Task<IActionResult> Edit(int ID, [FromBody] Job job)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 nếu dữ liệu không hợp lệ
            }

            var work = await _context.Job.FindAsync(ID);
            if (work == null)
            {
                return NotFound(); // 404 nếu không tìm thấy
            }

            work.Job_Name = job.Job_Name;
            work.Working_Experience_Require = job.Working_Experience_Require;
            work.Working_Address = job.Working_Address;

            _context.Job.Update(work);
            await _context.SaveChangesAsync();

            return NoContent(); // Trả về 204 No Content sau khi cập nhật thành công
        }


        // DELETE: api/Job/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(int id)
        {
            var job = await _context.Job.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }

            _context.Job.Remove(job);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Job/ByRecruiter/5
        [HttpGet("ByRecruiter/{recruiterId}")]
        public async Task<ActionResult<IEnumerable<Job>>> GetJobByRecruiter(int recruiterId)
        {
            try
            {
                // Check if the recruiter exists
                var recruiterExists = await _context.Recruiter.FindAsync(recruiterId);
                if (recruiterExists == null)
                {
                    return NotFound($"Recruiter with ID {recruiterId} does not exist.");
                }

                // Get all Job for this recruiter
                var Job = await _context.Job
                    .Where(j => j.ID_Recruiter == recruiterId)
                    .ToListAsync();

                return Ok(Job);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
