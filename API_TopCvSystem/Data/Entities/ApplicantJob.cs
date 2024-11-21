using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TopCVSystemAPIdotnet.Data.Entities
{
    public class ApplicantJob
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int ID_Job { get; set; } = 0;
        public int ID_Applicant { get; set; } = 0;
        public int ID_Resume { get; set; } = 0;
        public bool isAccepted { get; set; } = false;
        public bool isRejected { get; set; } = false;
        public string Time { get; set; } = "";
    }
}

