using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TopCVSystemAPIdotnet.Data.Entities
{
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int Sender_ID { get; set; } = 0;
        public int Receiver_ID { get; set; } = 0;
        public string Content { get; set; } = "";
        public Boolean Status { get; set; } = false; 
        public DateTime Send_Time { get; set; } = DateTime.Now;
        
    }
}
