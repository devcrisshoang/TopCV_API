using API_TopCvSystem.DTO;
using TopCVSystemAPIdotnet.Data.Entities;
using TopCVSystemAPIdotnet.DTOs;
using static System.Net.Mime.MediaTypeNames;

namespace API_TopCvSystem.Mapper
{
    public static class ResumeMapper
    {
        public static ResumeDto ToDto(Resume resume)
        {
            return new ResumeDto
            {
                ID = resume.ID,
                Applicant_Name = resume.Applicant_Name,
                Email = resume.Email,
                Phone_Number = resume.Phone_Number,
                Education = resume.Education,
                Skills = resume.Skills,
                Certificate = resume.Certificate,
                Job_Applying = resume.Job_Applying,
                Introduction = resume.Introduction,
                Image = resume.Image,
                Experience = resume.Experience,
            };
        }
    }
}
