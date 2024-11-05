using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TopCVSystemAPIdotnet.Data.Entities
{
    public class Job
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Image_Id { get; set; } = "";
        public string Job_Name { get; set; } = "";
        public string Company_Name { get; set; } = "";
        public string Working_Experience_Require { get; set; } = "";
        public string Working_Address { get; set; } = "";
        public string Salary { get; set; } = "";
        public DateTime Create_Time { get; set; } = DateTime.Now;
        public DateTime Application_Date { get; set; } = DateTime.Now;
        public Boolean Application_Status { get; set; } = false;
        public int ID_Recruiter { get; set; } = 0;
    }                                     
}
