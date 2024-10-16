using API_TopCvSystem.DTO;
using TopCVSystemAPIdotnet.Data.Entities;

namespace API_TopCvSystem.Mapper
{
    public static class CompanyMapper
    {
        public static CompanyDto ToDto(Company company)
        {
            return new CompanyDto
            {
                ID = company.ID,
                Company_Name = company.Company_Name,
                Company_Address = company.Company_Address,
                Hotline = company.Hotline,
                Field = company.Field,
                Image = company.Image,
                Green_Badge = company.Green_Badge,
            };
        }
    }
}
