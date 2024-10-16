using API_TopCvSystem.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TopCVSystemAPIdotnet.Data;
using TopCVSystemAPIdotnet.Data.Entities;
using TopCVSystemAPIdotnet.Data.Mappers;

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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                // Lấy tất cả các Recruiter từ database
                var recruiters = await _context.Recruiter.ToListAsync();

                // Kiểm tra nếu danh sách Recruiter rỗng
                if (recruiters == null || recruiters.Count == 0)
                {
                    return NotFound("No recruiters found."); // Trả về 404 nếu không có dữ liệu
                }

                // Trả về danh sách Recruiter
                return Ok(recruiters);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và trả về lỗi 500
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        // Lấy Recruiter theo ID
        [HttpGet("{ID}")]
        public async Task<IActionResult> GetByID(int ID)
        {
            try
            {
                var recruiter = await _context.Recruiter.FindAsync(ID); // Thay đổi từ _context.Recruiter thành _context.Recruiter
                if (recruiter == null)
                {
                    return NotFound(); // 404
                }
                var RecruiterDto = RecruiterMapper.ToDto(recruiter);
                return Ok(RecruiterDto);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        // Tạo mới Recruiter
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RecruiterDto RecruiterDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 nếu dữ liệu không hợp lệ
            }

            var newRecruiter = new Recruiter
            {
                Recruiter_Name = RecruiterDto.Recruiter_Name,
                Phone_Number = RecruiterDto.Phone_Number,
                Email_Address = RecruiterDto.Email_Address,
            };

            _context.Recruiter.Add(newRecruiter); 
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetByID), new { ID = newRecruiter.ID }, RecruiterMapper.ToDto(newRecruiter)); // Trả về 201 Created
        }

        // Cập nhật Recruiter
        [HttpPut("{ID}")]
        public async Task<IActionResult> Edit(int ID, [FromBody] RecruiterDto RecruiterDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 nếu dữ liệu không hợp lệ
            }

            var recruiter = await _context.Recruiter.FindAsync(ID); // Thay đổi từ _context.Recruiter thành _context.Recruiter
            if (recruiter == null)
            {
                return NotFound(); // 404 nếu không tìm thấy
            }

            // Cập nhật thông tin
            recruiter.Recruiter_Name = RecruiterDto.Recruiter_Name;
            recruiter.Phone_Number = RecruiterDto.Phone_Number;
            recruiter.Email_Address = RecruiterDto.Email_Address;

            _context.Recruiter.Update(recruiter); // Thay đổi từ _context.Recruiter.Update thành _context.Recruiter.Update
            await _context.SaveChangesAsync();

            return NoContent(); // Trả về 204 No Content sau khi cập nhật thành công
        }

        // Xóa Recruiter theo ID
        [HttpDelete("{ID}")]
        public async Task<IActionResult> Remove(int ID)
        {
            try
            {
                var recruiter = await _context.Recruiter.FindAsync(ID); // Thay đổi từ _context.Recruiter thành _context.Recruiter
                if (recruiter == null)
                {
                    return NotFound(); // 404 nếu không tìm thấy
                }

                _context.Recruiter.Remove(recruiter); // Thay đổi từ _context.Recruiter.Remove thành _context.Recruiter.Remove
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
