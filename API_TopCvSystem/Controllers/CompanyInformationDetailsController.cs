using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TopCVSystemAPIdotnet.Data;
using TopCVSystemAPIdotnet.Data.Entities;
using System.Threading.Tasks;
using API_TopCvSystem.DTO;

namespace TopCVSystemAPIdotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyInformationDetailsController : ControllerBase
    {
        private readonly TopCvDbContext _context;

        public CompanyInformationDetailsController(TopCvDbContext context)
        {
            _context = context;
        }

        // GET: api/CompanyInformationDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyInformationDetails>>> GetCompanyInformationDetails()
        {
            return await _context.CompanyInformationDetail.ToListAsync();
        }

        // GET: api/CompanyInformationDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyInformationDetails>> GetCompanyInformationDetails(int id)
        {
            var companyInformationDetails = await _context.CompanyInformationDetail.FindAsync(id);

            if (companyInformationDetails == null)
            {
                return NotFound();
            }

            return companyInformationDetails;
        }

        // GET: api/CompanyInformationDetails/ByCompany/5
        [HttpGet("ByCompany/{companyId}")]
        public async Task<ActionResult<IEnumerable<CompanyInformationDetails>>> GetCompanyInformationDetailsByCompanyId(int companyId)
        {
            try
            {
                // Check if the Company with the specified ID exists in the Company table
                var existingCompany = await _context.Company.FindAsync(companyId);

                if (existingCompany == null)
                {
                    // If the Company does not exist, return a NotFound response
                    return NotFound($"Company with ID {companyId} does not exist.");
                }

                // Get all CompanyInformationDetails that have the matching ID_Company
                var companyInformationDetails = await _context.CompanyInformationDetail
                    .Where(c => c.ID_Company == companyId)
                    .ToListAsync();

                // Check if any details were found
                if (companyInformationDetails == null || companyInformationDetails.Count == 0)
                {
                    // If no CompanyInformationDetails are found, return a NotFound response
                    return NotFound($"No Company Information Details found for Company with ID {companyId}.");
                }

                // Return the list of CompanyInformationDetails
                return Ok(companyInformationDetails);
            }
            catch (Exception ex)
            {
                // Catch any exceptions and return a 500 Internal Server Error response with the exception message
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        // POST: api/CompanyInformationDetails
        [HttpPost("{companyId}")]
        public async Task<ActionResult<CompanyInformationDetails>> PostCompanyInformationDetails(int companyId, CompanyInformationDetails companyInformationDetails)
        {
            try
            {
                // Check if the Company with the specified ID exists in the Company table
                var existingCompany = await _context.Company.FindAsync(companyId);

                if (existingCompany == null)
                {
                    // If the Company does not exist, return a BadRequest response
                    return BadRequest($"Company with ID {companyId} does not exist.");
                }

                // Check if a CompanyInformationDetails with the same ID_Company already exists
                var existingCompanyInformation = await _context.CompanyInformationDetail
                    .FirstOrDefaultAsync(c => c.ID_Company == companyId);

                if (existingCompanyInformation != null)
                {
                    // If the ID_Company already exists in CompanyInformationDetails, return a BadRequest response
                    return BadRequest($"Company information for Company ID {companyId} already exists.");
                }

                // Assign the companyId to the new CompanyInformationDetails object
                companyInformationDetails.ID_Company = companyId;

                // Add the new CompanyInformationDetails entry
                _context.CompanyInformationDetail.Add(companyInformationDetails);
                await _context.SaveChangesAsync();

                // Return the created response with the new company information details
                return CreatedAtAction(nameof(GetCompanyInformationDetails), new { id = companyInformationDetails.ID_Company_Information_Details }, companyInformationDetails);
            }
            catch (Exception ex)
            {
                // Catch any exceptions and return a 500 Internal Server Error response with the exception message
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        // Cập nhật Company
        [HttpPut("{ID}")]
        public async Task<IActionResult> Edit(int ID, [FromBody] CompanyInformationDetails companyInformation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 nếu dữ liệu không hợp lệ
            }

            var infor = await _context.CompanyInformationDetail.FindAsync(ID);
            if (infor == null)
            {
                return NotFound(); // 404 nếu không tìm thấy
            }

            infor.TaxID = companyInformation.TaxID;
            infor.Website = companyInformation.Website;

            _context.CompanyInformationDetail.Update(infor);
            await _context.SaveChangesAsync();

            return NoContent(); // Trả về 204 No Content sau khi cập nhật thành công
        }

        // DELETE: api/CompanyInformationDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompanyInformationDetails(int id)
        {
            var companyInformationDetails = await _context.CompanyInformationDetail.FindAsync(id);
            if (companyInformationDetails == null)
            {
                return NotFound();
            }

            _context.CompanyInformationDetail.Remove(companyInformationDetails);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompanyInformationDetailsExists(int id)
        {
            return _context.CompanyInformationDetail.Any(e => e.ID_Company_Information_Details == id);
        }
    }
}
