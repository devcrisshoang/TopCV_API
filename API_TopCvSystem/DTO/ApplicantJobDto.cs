namespace TopCVSystemAPIdotnet.DTOs
{
    public class ApplicantJobDto
    {
        public int ID { get; set; }
        public string Applicant_Name { get; set; }
        public string Phone_Number { get; set; }
        public string Email { get; set; }
        public bool isAccepted { get; set; }
        public bool isRejected {  get; set; }
        public string Time { get; set; }
    }
}