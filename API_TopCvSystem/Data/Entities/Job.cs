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
        public int Salary { get; set; } = 0;
        public float rate { get; set; } = 0;
        public string Create_Time { get; set; } = "";
        public string Application_Date { get; set; } = "";
        public Boolean Application_Status { get; set; } = false;
        public int ID_Recruiter { get; set; } = 0;
    }                                     
}
