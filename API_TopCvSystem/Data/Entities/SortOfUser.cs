using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TopCVSystemAPIdotnet.Data.Entities
{
    public class SortOfUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID_SortOfUser { get; set; }
        public int Recruiter { get; set; } = 0;
        public int Applicant { get; set; } = 0;
        public int Admin { get; set; } = 0;
        public int ID_User { get; set; }
    }
}
