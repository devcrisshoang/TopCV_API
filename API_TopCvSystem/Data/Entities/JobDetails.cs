using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TopCVSystemAPIdotnet.Data.Entities
{
    public class JobDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Job_Description { get; set; }
        public string Skill_Require { get; set; }
        public string Benefit { get; set; }
        public string Gender_Require { get; set; }
        public string Working_Time { get; set; }
        public string Working_Method { get; set; }
        public string Working_Position { get; set; }
        public int ID_Job { get; set; } 
    }
}
