using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TopCVSystemAPIdotnet.Data.Entities
{
    public class Resume
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Applicant_Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone_Number { get; set; } = "";
        public string Education { get; set; } = "";
        public string Skills { get; set; } = "";
        public string Certificate { get; set; } = "";
        public string Job_Applying { get; set; } = "";
        public string Introduction { get; set; } = "";
        public string Image { get; set; } = "";
        public string Experience { get; set; } = "";
        public int ID_Applicant { get; set; } = 0;
        public string File_Path { get; set; } = "";
    }
}
