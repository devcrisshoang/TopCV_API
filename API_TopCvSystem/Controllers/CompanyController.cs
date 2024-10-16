using API_TopCvSystem.DTO;
using API_TopCvSystem.Mapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TopCVSystemAPIdotnet.Data;
using TopCVSystemAPIdotnet.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace API_Mobile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly TopCvDbContext _context;

        public CompanyController(TopCvDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCompany()
        {
            try
            {
                // Lấy tất cả các Company từ database
                var companies = await _context.Company.ToListAsync();

                // Kiểm tra nếu danh sách rỗng
                if (companies == null || companies.Count == 0)
                {
                    return NotFound("No companies found."); // Trả về 404 nếu không có công ty nào
                }

                // Trả về danh sách các Company
                return Ok(companies);
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ và trả về mã lỗi 500
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet("recruiter/{recruiterId}")]
        public async Task<IActionResult> GetCompanyByRecruiterId(int recruiterId)
        {
            try
            {
                // Tìm Recruiter dựa trên ID
                var recruiter = await _context.Recruiter.FirstOrDefaultAsync(r => r.ID == recruiterId);

                // Kiểm tra nếu không tìm thấy Recruiter
                if (recruiter == null)
                {
                    return NotFound($"Recruiter with ID {recruiterId} not found.");
                }

                // Tìm công ty dựa trên ID_Company của Recruiter
                var company = await _context.Company.FindAsync(recruiter.ID_Company);

                // Kiểm tra nếu không tìm thấy công ty
                if (company == null)
                {
                    return NotFound($"Company for recruiter with ID {recruiterId} not found.");
                }

                // Trả về thông tin công ty
                return Ok(company);
            }
            catch (Exception ex)
            {
                // Trả về mã lỗi 500 nếu có lỗi
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            try
            {
                var company = await _context.Company.FindAsync(id);
                if (company == null)
                {
                    return NotFound(); // 404 nếu không tìm thấy
                }

                return Ok(company); // Trả về dữ liệu công ty
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPost("recruiter/{recruiterId}")]
        public async Task<IActionResult> CreateCompanyForRecruiter(int recruiterId, [FromBody] CompanyDto companyDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 nếu dữ liệu không hợp lệ
            }

            try
            {
                // Tìm Recruiter dựa trên recruiterId
                var recruiter = await _context.Recruiter.FirstOrDefaultAsync(r => r.ID == recruiterId);

                // Kiểm tra nếu không tìm thấy Recruiter
                if (recruiter == null)
                {
                    return NotFound($"Recruiter with ID {recruiterId} not found.");
                }

                // Tạo mới một công ty
                var newCompany = new Company
                {
                    Company_Name = companyDto.Company_Name,
                    Company_Address = companyDto.Company_Address,
                    Hotline = companyDto.Hotline,
                    Field = companyDto.Field,
                    Image = companyDto.Image,
                    Green_Badge = companyDto.Green_Badge,
                };

                // Thêm công ty mới vào database
                _context.Company.Add(newCompany);
                await _context.SaveChangesAsync();  // Lưu lại để có được ID của công ty

                // Cập nhật ID_Company của Recruiter với ID của công ty vừa tạo
                recruiter.ID_Company = newCompany.ID;

                // Lưu lại sự thay đổi của Recruiter
                _context.Recruiter.Update(recruiter);
                await _context.SaveChangesAsync();

                // Trả về thông tin công ty vừa được tạo
                return CreatedAtAction(nameof(GetByID), new { ID = newCompany.ID }, newCompany);
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ và trả về lỗi 500
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        // Cập nhật Company
        [HttpPut("{ID}")]
        public async Task<IActionResult> Edit(int ID, [FromBody] CompanyDto companyDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 nếu dữ liệu không hợp lệ
            }

            var company = await _context.Company.FindAsync(ID);
            if (company == null)
            {
                return NotFound(); // 404 nếu không tìm thấy
            }

            company.Company_Name = companyDto.Company_Name;
            company.Company_Address = companyDto.Company_Address;
            company.Hotline = companyDto.Hotline;
            company.Field = companyDto.Field;
            company.Image = companyDto.Image;
            company.Green_Badge = companyDto.Green_Badge;

            _context.Company.Update(company);
            await _context.SaveChangesAsync();

            return NoContent(); // Trả về 204 No Content sau khi cập nhật thành công
        }

        // Xóa Company theo ID
        [HttpDelete("{ID}")]
        public async Task<IActionResult> Remove(int ID)
        {
            try
            {
                var company = await _context.Company.FindAsync(ID);
                if (company == null)
                {
                    return NotFound(); // 404 nếu không tìm thấy
                }

                _context.Company.Remove(company);
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
