using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TopCVSystemAPIdotnet.Data.Entities
{
    public class Recruiter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Recruiter_Name { get; set; } = "";
        public string Phone_Number { get; set; } = "";
        public int ID_Company { get; set; } = 0;
        public string Email_Address { get; set; } = "";
    }
}
