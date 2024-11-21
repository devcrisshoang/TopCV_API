namespace TopCVSystemAPIdotnet.DTOs
{
    public class JobDto
    {
        public int ID { get; set; }
        public string Image_Id { get; set; } 
        public string Job_Name { get; set; }
        public string Company_Name { get; set; }
        public string Working_Experience_Require { get; set; }
        public string Working_Address { get; set; }
        public int Salary { get; set; }
        public string Create_Time { get; set; }
        public string Application_Date { get; set; }
        public Boolean Application_Status { get; set; }

    }
}
