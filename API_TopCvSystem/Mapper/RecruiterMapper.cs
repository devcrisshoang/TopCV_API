using API_TopCvSystem.DTO;
using TopCVSystemAPIdotnet.Data.Entities;

namespace TopCVSystemAPIdotnet.Data.Mappers
{
    public static class RecruiterMapper
    {
        public static RecruiterDto ToDto(Recruiter recruiter)
        {
            return new RecruiterDto
            {
                ID = recruiter.ID,
                Recruiter_Name = recruiter.Recruiter_Name,
                Phone_Number = recruiter.Phone_Number,
                Email_Address = recruiter.Email_Address,
            };
        }
    }
}
