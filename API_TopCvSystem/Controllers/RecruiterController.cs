using API_TopCvSystem.Mapper;
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
    public class RecruiterController : ControllerBase
    {
        private readonly TopCvDbContext _context;

        public RecruiterController(TopCvDbContext context)
        {
            _context = context;
        }

        // Lấy tất cả Recruiter
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var recruiters = await _context.Recruiter.ToListAsync();

                if (recruiters == null || recruiters.Count == 0)
                {
                    return NotFound("No recruiters found.");
                }

                return Ok(recruiters); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Lấy Recruiter theo ID
        [HttpGet("{ID}")]
        public async Task<IActionResult> GetByID(int ID)
        {
            try
            {
                var recruiter = await _context.Recruiter.FindAsync(ID);
                if (recruiter == null)
                {
                    return NotFound();
                }

                return Ok(recruiter); // Trả về trực tiếp đối tượng Recruiter
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        // Lấy Recruiter theo ID_User
        [HttpGet("user/{ID_User}")]
        public async Task<IActionResult> GetByUserID(int ID_User)
        {
            try
            {
                var recruiter = await _context.Recruiter.FirstOrDefaultAsync(a => a.ID_User == ID_User);
                if (recruiter == null)
                {
                    return NotFound();
                }
                return Ok(recruiter);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }


        // Tạo mới Recruiter
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Recruiter recruiter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newRecruiter = new Recruiter
            {
                Recruiter_Name = recruiter.Recruiter_Name,
                Phone_Number = recruiter.Phone_Number,
                Email_Address = recruiter.Email_Address,
                ID_Company = recruiter.ID_Company,
                ID_User = recruiter.ID_User,
                Front_Image = recruiter.Front_Image,
                Back_Image = recruiter.Back_Image
            };

            _context.Recruiter.Add(newRecruiter);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetByID), new { ID = newRecruiter.ID }, newRecruiter); // Trả về 201 Created với đối tượng vừa tạo
        }

        [HttpPut("UpdateStatus/{ID}")]
        public async Task<IActionResult> UpdateStatus(int ID, [FromBody] Recruiter recruiterData)
        {
            try
            {
                // Tìm Recruiter theo ID
                var recruiter = await _context.Recruiter.FindAsync(ID);
                if (recruiter == null)
                {
                    return NotFound($"Recruiter with ID {ID} not found.");
                }

                // Cập nhật Is_Registered và Is_Confirm từ dữ liệu gửi lên
                recruiter.Is_Registered = recruiterData.Is_Registered;
                recruiter.Is_Confirm = recruiterData.Is_Confirm;

                // Lưu các thay đổi vào cơ sở dữ liệu
                _context.Recruiter.Update(recruiter);
                await _context.SaveChangesAsync();

                // Trả về kết quả xác nhận hoặc không xác nhận
                if (recruiter.Is_Confirm)
                {
                    return Ok("Recruiter status confirmed successfully.");
                }
                else
                {
                    return Ok("Recruiter confirmation removed successfully.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        // Cập nhật Recruiter
        [HttpPut("{ID}")]
        public async Task<IActionResult> Edit(int ID, [FromBody] Recruiter recruiter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingRecruiter = await _context.Recruiter.FindAsync(ID);
            if (existingRecruiter == null)
            {
                return NotFound();
            }

            // Cập nhật các thông tin
            existingRecruiter.Recruiter_Name = recruiter.Recruiter_Name;
            existingRecruiter.Phone_Number = recruiter.Phone_Number;
            existingRecruiter.Email_Address = recruiter.Email_Address;
            existingRecruiter.ID_Company = recruiter.ID_Company;
            existingRecruiter.ID_User = recruiter.ID_User;
            existingRecruiter.Front_Image = recruiter.Front_Image;
            existingRecruiter.Back_Image = recruiter.Back_Image;
            existingRecruiter.Is_Registered = recruiter.Is_Registered;
            existingRecruiter.Is_Confirm = recruiter.Is_Confirm;

            _context.Recruiter.Update(existingRecruiter);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Xóa Recruiter theo ID
        [HttpDelete("{ID}")]
        public async Task<IActionResult> Remove(int ID)
        {
            try
            {
                var recruiter = await _context.Recruiter.FindAsync(ID);
                if (recruiter == null)
                {
                    return NotFound();
                }

                _context.Recruiter.Remove(recruiter);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }
        #region Suggest Applicants
        //Lay danh sach goi y applicant
        [HttpGet("{ID}/SuggestedApplicants")]
        public async Task<IActionResult> GetSuggestedApplicants(int ID)
        {
            try
            {
                var recruiter = await _context.Recruiter.FindAsync(ID);
                if (recruiter == null)
                {
                    return NotFound("Recruiter not found.");
                }

                // Lấy danh sách các công việc của Recruiter
                var jobIds = await _context.Job
                    .Where(j => j.ID_Recruiter == ID)
                    .Select(j => new { j.ID, j.Job_Name })
                    .ToListAsync();

                if (!jobIds.Any())
                {
                    return Ok(new List<ApplicantDto>());
                }

                // Tạo danh sách Job_Name và Job_ID
                var jobNames = jobIds.Select(j => j.Job_Name).ToList();
                var jobIdList = jobIds.Select(j => j.ID).ToList();

                // Lấy danh sách các Applicant có Job_Desire trùng với Job_Name và chưa ứng tuyển hoặc chưa được chấp nhận
                var suggestedApplicants = await _context.Applicant
                    .Where(a => jobNames.Contains(a.Job_Desire) &&
                                _context.ApplicantJob.Any(aj => aj.ID_Applicant == a.ID))
                    .ToListAsync();

                // Chuyển đổi sang DTO nếu cần
                var applicantDtos = suggestedApplicants.Select(a => ApplicantMapper.ToDto(a)).ToList();

                return Ok(applicantDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        #endregion

        #region Applicants For Recruiter

        //Lấy danh sách applicant ung tuyen cho job cua recruiter
        [HttpGet("{recruiterId}/ApplicantsForJobToList")]
        public async Task<IActionResult> GetApplicantsForJobs(int recruiterId)
        {
            try
            {
                // Tìm các công việc của Recruiter
                var jobIds = await _context.Job
                    .Where(j => j.ID_Recruiter == recruiterId)
                    .Select(j => j.ID)
                    .ToListAsync();

                if (!jobIds.Any())
                {
                    return Ok(new List<ApplicantJobDto>());
                }

                // Lấy danh sách các ApplicantJob liên quan đến các job của Recruiter
                var applicantJobs = await _context.ApplicantJob
                    .Where(aj => jobIds.Contains(aj.ID_Job))
                    .ToListAsync();

                // Lấy thông tin của các Applicant tương ứng với các ApplicantJob
                var applicantIds = applicantJobs.Select(aj => aj.ID_Applicant).Distinct().ToList();
                var applicants = await _context.Applicant
                    .Where(a => applicantIds.Contains(a.ID))
                    .ToListAsync();

                // Kết hợp dữ liệu Applicant và ApplicantJob vào một danh sách DTO
                var applicantJobDtos = applicantJobs
                    .Join(applicants,
                        aj => aj.ID_Applicant,
                        a => a.ID,
                        (aj, a) => new ApplicantJobDto
                        {
                            ID = a.ID,
                            Applicant_Name = a.Applicant_Name,
                            Phone_Number = a.Phone_Number,
                            Email = a.Email,
                            isAccepted = aj.isAccepted,
                            isRejected = aj.isRejected,
                            Time = aj.Time
                        })
                    .ToList();

                return Ok(applicantJobDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        #endregion

        #region CV Applicant For Recruiter
        //Lay danh sach CV cua applicant nop cv cho recruiter
        [HttpGet("{recruiterId}/applicant/{applicantId}/cv")]
        public async Task<IActionResult> GetApplicantCvForRecruiter(int recruiterId, int applicantId)
        {
            try
            {
                // Kiểm tra Recruiter có tồn tại không
                var recruiter = await _context.Recruiter.FindAsync(recruiterId);
                if (recruiter == null)
                {
                    return NotFound("Recruiter not found.");
                }

                // Lấy danh sách Job của Recruiter
                var jobIds = await _context.Job
                    .Where(j => j.ID_Recruiter == recruiterId)
                    .Select(j => j.ID)
                    .ToListAsync();

                if (!jobIds.Any())
                {
                    return NotFound("No jobs found for this recruiter.");
                }

                // Kiểm tra xem Applicant có ứng tuyển vào Job nào của Recruiter không
                var applicantJob = await _context.ApplicantJob
                    .Where(aj => aj.ID_Applicant == applicantId && jobIds.Contains(aj.ID_Job))
                    .FirstOrDefaultAsync();

                if (applicantJob == null)
                {
                    return NotFound("Applicant has not applied to any job for this recruiter.");
                }

                // Lấy CV (Resume) của Applicant
                var resume = await _context.Resume
                    .Where(r => r.ID_Applicant == applicantId)
                    .FirstOrDefaultAsync();

                if (resume == null)
                {
                    return NotFound("CV not found for this applicant.");
                }

                // Chuyển đổi Resume sang DTO
                var resumeDto = ResumeMapper.ToDto(resume); // Giả sử bạn có ResumeMapper để chuyển đổi sang DTO

                return Ok(resumeDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        #endregion

        #region Accepted
        [HttpGet("{recruiterId}/AcceptedApplicants")]
        public async Task<IActionResult> GetAcceptedApplicants(int recruiterId)
        {
            try
            {
                // Tìm các công việc của Recruiter
                var jobIds = await _context.Job
                    .Where(j => j.ID_Recruiter == recruiterId)
                    .Select(j => j.ID)
                    .ToListAsync();

                if (!jobIds.Any())
                {
                    return Ok(new List<ApplicantJobDto>());
                }

                // Lấy danh sách ApplicantJob cho các job của Recruiter và có isAccepted = true
                var acceptedApplicantJobs = await _context.ApplicantJob
                    .Where(aj => jobIds.Contains(aj.ID_Job) && aj.isAccepted && !aj.isRejected)
                    .ToListAsync();

                if (!acceptedApplicantJobs.Any())
                {
                    return Ok(new List<ApplicantJobDto>());
                }

                // Lấy thông tin của các Applicant tương ứng với các ApplicantJob
                var applicantIds = acceptedApplicantJobs.Select(aj => aj.ID_Applicant).Distinct().ToList();
                var applicants = await _context.Applicant
                    .Where(a => applicantIds.Contains(a.ID))
                    .ToListAsync();

                // Kết hợp dữ liệu Applicant và ApplicantJob vào một danh sách DTO (ApplicantJobDto)
                var applicantJobDtos = acceptedApplicantJobs
                    .Join(applicants,
                        aj => aj.ID_Applicant,
                        a => a.ID,
                        (aj, a) => new ApplicantJobDto
                        {
                            ID = a.ID,
                            Applicant_Name = a.Applicant_Name,
                            Phone_Number = a.Phone_Number,
                            Email = a.Email,
                            isAccepted = aj.isAccepted,
                            isRejected = aj.isRejected,
                            Time = aj.Time
                        })
                    .ToList();

                return Ok(applicantJobDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPut("{recruiterId}/applicants/{applicantId}/acceptance")]
        public async Task<IActionResult> UpdateAcceptanceStatus(int recruiterId, int applicantId, [FromBody] bool isAccepted)
        {
            try
            {
                // Kiểm tra xem Recruiter có tồn tại không
                var recruiter = await _context.Recruiter.FindAsync(recruiterId);
                if (recruiter == null)
                {
                    return NotFound("Recruiter not found.");
                }

                // Kiểm tra xem Applicant có tồn tại không
                var applicant = await _context.Applicant.FindAsync(applicantId);
                if (applicant == null)
                {
                    return NotFound("Applicant not found.");
                }

                // Lấy danh sách các Job của Recruiter
                var jobIds = await _context.Job
                    .Where(j => j.ID_Recruiter == recruiterId)
                    .Select(j => j.ID)
                    .ToListAsync();

                if (!jobIds.Any())
                {
                    return BadRequest("No jobs found for the recruiter.");
                }

                // Tìm bản ghi trong bảng Applicant_Job liên quan đến applicant và các job của recruiter
                var applicantJob = await _context.ApplicantJob
                    .Where(aj => aj.ID_Applicant == applicantId && jobIds.Contains(aj.ID_Job))
                    .FirstOrDefaultAsync();

                if (applicantJob == null)
                {
                    return NotFound("Applicant is not associated with any jobs of this recruiter.");
                }

                // Cập nhật giá trị isAccepted và isRejected
                if (isAccepted)
                {
                    applicantJob.isAccepted = true;
                    applicantJob.isRejected = false; // Đảm bảo trạng thái rejected là false khi được accepted
                }
                else
                {
                    applicantJob.isAccepted = false;
                    applicantJob.isRejected = true; // Đảm bảo trạng thái accepted là false khi bị rejected
                }

                // Lưu thay đổi vào cơ sở dữ liệu
                _context.ApplicantJob.Update(applicantJob);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Applicant status updated successfully.", IsAccepted = applicantJob.isAccepted, IsRejected = applicantJob.isRejected });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        #endregion

        #region Rejected
        [HttpGet("{recruiterId}/RejectedApplicants")]
        public async Task<IActionResult> GetRejectedApplicants(int recruiterId)
        {
            try
            {
                // Tìm các công việc của Recruiter
                var jobIds = await _context.Job
                    .Where(j => j.ID_Recruiter == recruiterId)
                    .Select(j => j.ID)
                    .ToListAsync();

                if (!jobIds.Any())
                {
                    return Ok(new List<ApplicantJobDto>());
                }

                // Lấy danh sách ApplicantJob cho các job của Recruiter và có isRejected = true
                var rejectedApplicantJobs = await _context.ApplicantJob
                    .Where(aj => jobIds.Contains(aj.ID_Job) && !aj.isAccepted && aj.isRejected)
                    .ToListAsync();

                if (!rejectedApplicantJobs.Any())
                {
                    return Ok(new List<ApplicantJobDto>());
                }

                // Lấy thông tin của các Applicant tương ứng với các ApplicantJob
                var applicantIds = rejectedApplicantJobs.Select(aj => aj.ID_Applicant).Distinct().ToList();
                var applicants = await _context.Applicant
                    .Where(a => applicantIds.Contains(a.ID))
                    .ToListAsync();

                // Kết hợp dữ liệu Applicant và ApplicantJob vào một danh sách DTO (ApplicantJobDto)
                var applicantJobDtos = rejectedApplicantJobs
                    .Join(applicants,
                        aj => aj.ID_Applicant,
                        a => a.ID,
                        (aj, a) => new ApplicantJobDto
                        {
                            ID = a.ID,
                            Applicant_Name = a.Applicant_Name,
                            Phone_Number = a.Phone_Number,
                            Email = a.Email,
                            isAccepted = aj.isAccepted,
                            isRejected = aj.isRejected,
                            Time = aj.Time
                        })
                    .ToList();

                return Ok(applicantJobDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        #endregion

        #region Job
        [HttpGet("recruiter/{recruiterId}/job/{jobId}/applicants")]
        public async Task<IActionResult> GetApplicantsForSpecificJob(int recruiterId, int jobId)
        {
            try
            {
                // Kiểm tra xem công việc có thuộc về Recruiter không
                var job = await _context.Job
                    .Where(j => j.ID == jobId && j.ID_Recruiter == recruiterId)
                    .FirstOrDefaultAsync();

                if (job == null)
                {
                    return NotFound("Job not found or does not belong to the specified recruiter.");
                }

                // Lấy danh sách các ApplicantJob liên quan đến jobId với isAccepted = false
                var applicantJobs = await _context.ApplicantJob
                    .Where(aj => aj.ID_Job == jobId && !aj.isAccepted && !aj.isRejected)
                    .ToListAsync();

                if (!applicantJobs.Any())
                {
                    return Ok(new List<ApplicantJobDto>());
                }

                // Lấy thông tin của các Applicant tương ứng với các ApplicantJob
                var applicantIds = applicantJobs.Select(aj => aj.ID_Applicant).Distinct().ToList();
                var applicants = await _context.Applicant
                    .Where(a => applicantIds.Contains(a.ID))
                    .ToListAsync();

                // Kết hợp dữ liệu Applicant và ApplicantJob vào một danh sách DTO
                var applicantJobDtos = applicantJobs
                    .Join(applicants,
                        aj => aj.ID_Applicant,
                        a => a.ID,
                        (aj, a) => new ApplicantJobDto
                        {
                            ID = a.ID,
                            Applicant_Name = a.Applicant_Name,
                            Phone_Number = a.Phone_Number,
                            Email = a.Email,
                            isAccepted = aj.isAccepted,
                            isRejected = aj.isRejected,
                            Time = aj.Time
                        })
                    .ToList();

                return Ok(applicantJobDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        #endregion
        [HttpPost("UploadImage/{ID}")]
        public async Task<IActionResult> UploadImage(int ID, IFormFile file, string imageType)
        {
            try
            {
                // Tìm Recruiter theo ID
                var recruiter = await _context.Recruiter.FindAsync(ID);
                if (recruiter == null)
                {
                    return NotFound("Recruiter not found.");
                }

                // Kiểm tra loại ảnh (Front_Image hoặc Back_Image)
                if (imageType != "Front_Image" && imageType != "Back_Image")
                {
                    return BadRequest("Invalid image type. Use 'Front_Image' or 'Back_Image'.");
                }

                // Kiểm tra file hợp lệ
                if (file == null || file.Length == 0)
                {
                    return BadRequest("No file uploaded.");
                }

                // Tạo thư mục lưu ảnh nếu chưa tồn tại
                var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(uploadsDir))
                {
                    Directory.CreateDirectory(uploadsDir);
                }

                // Đặt tên file duy nhất
                var fileName = $"{Guid.NewGuid()}_{file.FileName}";
                var filePath = Path.Combine(uploadsDir, fileName);

                // Lưu file vào server
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Tạo URL truy cập ảnh
                var fileUrl = $"{Request.Scheme}://{Request.Host}/uploads/{fileName}";

                // Lưu URL vào database
                if (imageType == "Front_Image")
                {
                    recruiter.Front_Image = fileUrl;
                }
                else
                {
                    recruiter.Back_Image = fileUrl;
                }

                _context.Recruiter.Update(recruiter);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Image uploaded successfully.", ImageUrl = fileUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("images/{recruiterID}")]
        public async Task<IActionResult> GetImages(int recruiterID)
        {
            // Lấy danh sách ảnh liên quan đến recruiterID
            var images = await _context.Images
                .Where(i => i.ImagePath.Contains($"recruiter_{recruiterID}"))
                .ToListAsync();

            if (images == null || !images.Any())
            {
                return NotFound("No images found for this recruiter.");
            }

            // Trả về danh sách các ảnh
            var imageUrls = images.Select(i => Url.Content($"~/images/{i.ImagePath}")).ToList();

            return Ok(imageUrls);
        }

    }
}
