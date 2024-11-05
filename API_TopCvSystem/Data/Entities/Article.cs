﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TopCVSystemAPIdotnet.Data.Entities
{
    public class Article
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Article_Name { get; set; } = "";
        public string Content { get; set; } = "";
        public DateTime Create_Time { get; set; } = DateTime.Now;
        public string image { get; set; } ="";
        public int ID_Recruiter { get; set; } = 0;
    }

}
