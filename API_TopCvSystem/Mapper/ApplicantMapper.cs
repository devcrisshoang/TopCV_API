using TopCVSystemAPIdotnet.Data.Entities;
using TopCVSystemAPIdotnet.DTOs;

namespace TopCVSystemAPIdotnet.Mappers
{
    public static class ApplicantMapper
    {
        public static ApplicantDto ToDto(Applicant applicant)
        {
            return new ApplicantDto
            {
                ID = applicant.ID,
                Applicant_Name = applicant.Applicant_Name,
                Phone_Number = applicant.Phone_Number,
                Email = applicant.Email,
                Job_Desire = applicant.Job_Desire,
                Working_Location_Desire = applicant.Working_Location_Desire,
                Working_Experience = applicant.Working_Experience,
            };
        }
    }
}
