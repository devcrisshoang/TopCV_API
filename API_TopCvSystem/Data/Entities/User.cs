using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TopCVSystemAPIdotnet.Data.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public string Image_Background { get; set; } = "";
        public string Avatar {  get; set; } = "";
        public int UID { get; set; } = 0;
        public bool IsApplicant { get; set; } = false;
        public bool IsRecruiter { get; set; } = false;
    }
}
