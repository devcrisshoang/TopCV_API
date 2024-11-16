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
        public int ID_User { get; set; } = 0;
        public string Front_Image { get; set; } = "";
        public string Back_Image { get; set; } = "";
        public Boolean Is_Registered { get; set; } = false;
        public Boolean Is_Confirm { get; set; } = false;
    }
}
