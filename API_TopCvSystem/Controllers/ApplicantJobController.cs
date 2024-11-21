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
    public class ApplicantJobController : ControllerBase
    {
        private readonly TopCvDbContext _context;

        public ApplicantJobController(TopCvDbContext context)
        {
            _context = context;
        }

        // GET: api/ApplicantJob
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicantJob>>> GetApplicantJobs()
        {
            return await _context.ApplicantJob.ToListAsync();
        }

        // GET: api/ApplicantJob/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicantJob>> GetApplicantJob(int id)
        {
            var applicantJob = await _context.ApplicantJob.FindAsync(id);

            if (applicantJob == null)
            {
                return NotFound();
            }

            return applicantJob;
        }

        // POST: api/ApplicantJob
        [HttpPost]
        public async Task<ActionResult<ApplicantJob>> PostApplicantJob(ApplicantJob ApplicantJob)
        {
            try
            {
                // Check if the Job with the specified ID exists
                var existingJob = await _context.Job.FindAsync(ApplicantJob.ID_Job);
                if (existingJob == null)
                {
                    return BadRequest($"Job with ID {ApplicantJob.ID_Job} does not exist.");
                }

                // Check if the Applicant with the specified ID exists
                var existingApplicant = await _context.Applicant.FindAsync(ApplicantJob.ID_Applicant);
                if (existingApplicant == null)
                {
                    return BadRequest($"Applicant with ID {ApplicantJob.ID_Applicant} does not exist.");
                }

                // Add the new ApplicantJob entry
                _context.ApplicantJob.Add(ApplicantJob);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetApplicantJob), new { id = ApplicantJob.ID }, ApplicantJob);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/ApplicantJob/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplicantJob(int id, ApplicantJob ApplicantJob)
        {
            if (id != ApplicantJob.ID)
            {
                return BadRequest("ApplicantJob ID mismatch.");
            }

            var existingApplicantJob = await _context.ApplicantJob.FindAsync(id);
            if (existingApplicantJob == null)
            {
                return NotFound();
            }

            // Check if the Job with the specified ID exists
            var existingJob = await _context.Job.FindAsync(ApplicantJob.ID_Job);
            if (existingJob == null)
            {
                return BadRequest($"Job with ID {ApplicantJob.ID_Job} does not exist.");
            }

            // Check if the Applicant with the specified ID exists
            var existingApplicant = await _context.Applicant.FindAsync(ApplicantJob.ID_Applicant);
            if (existingApplicant == null)
            {
                return BadRequest($"Applicant with ID {ApplicantJob.ID_Applicant} does not exist.");
            }

            // Update the ApplicantJob fields
            existingApplicantJob.ID_Job = ApplicantJob.ID_Job;
            existingApplicantJob.ID_Applicant = ApplicantJob.ID_Applicant;

            _context.Entry(existingApplicantJob).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicantJobExists(id))
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

        // DELETE: api/ApplicantJob/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplicantJob(int id)
        {
            var applicantJob = await _context.ApplicantJob.FindAsync(id);
            if (applicantJob == null)
            {
                return NotFound();
            }

            _context.ApplicantJob.Remove(applicantJob);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ApplicantJobExists(int id)
        {
            return _context.ApplicantJob.Any(e => e.ID == id);
        }
    }
}
