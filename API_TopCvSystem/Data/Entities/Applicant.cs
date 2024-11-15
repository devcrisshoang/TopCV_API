using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TopCVSystemAPIdotnet.Data.Entities
{
    public class Applicant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Applicant_Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone_Number { get; set; } = "";
        public string Job_Desire { get; set; } = "";
        public string Working_Location_Desire { get; set; } = "";
        public string Working_Experience { get; set; } = "";
        public int ID_User { get; set; } = 0;
        public Boolean Is_Registered { get; set; } = false;
    }
}
