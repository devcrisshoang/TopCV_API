using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TopCVSystemAPIdotnet.Data.Entities
{
    public class CompanyInformationDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID_Company_Information_Details { get; set; }
        public string Website { get; set; }
        public int TaxID { get; set; } = 0;
        public DateTime DateFounded { get; set; } = DateTime.UtcNow;
        public int ID_Company { get; set; } = 0;
    }
}
