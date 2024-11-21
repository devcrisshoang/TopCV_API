using TopCVSystemAPIdotnet.Data.Entities;
using TopCVSystemAPIdotnet.DTOs;

namespace TopCVSystemAPIdotnet.Mappers
{
    public static class JobMapper
    {
        public static JobDto ToDto(Job job)
        {
            return new JobDto
            {
                ID = job.ID,
                Image_Id = job.Image_Id,
                Job_Name = job.Job_Name,
                Working_Experience_Require = job.Working_Experience_Require,
                Working_Address = job.Working_Address,
                Salary = job.Salary,
                Create_Time = job.Create_Time,
                Application_Date = job.Application_Date,
                Application_Status = job.Application_Status,
                
            };
        }
    }
}