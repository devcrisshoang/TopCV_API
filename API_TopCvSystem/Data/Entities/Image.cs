using System;
using System.ComponentModel.DataAnnotations;

namespace TopCVSystemAPIdotnet.Data.Entities
{
    public class Image
    {
        [Key]
        public int ID { get; set; }

        public string ImageName { get; set; } = "";  // Tên ảnh
        public string ImagePath { get; set; } = "";  // Đường dẫn ảnh
        public DateTime UploadedDate { get; set; }   // Ngày upload ảnh
    }
}