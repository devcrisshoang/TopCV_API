using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TopCVSystemAPIdotnet.Data.Entities
{
    public class Company
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Company_Name { get; set; } = "";
        public string Company_Address { get; set; } = "";
        public string Hotline { get; set; } = "";
        public string Field { get; set; } = "";
        public string Image { get; set; } = "";
        public bool Green_Badge { get; set; } = false;
    }
}
