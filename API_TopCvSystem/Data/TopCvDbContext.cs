using API_TopCvSystem.Data.Entities;
using API_TopCvSystem.Migrations;
using Microsoft.EntityFrameworkCore;
using TopCVSystemAPIdotnet.Controllers;
using TopCVSystemAPIdotnet.Data.Entities;

namespace TopCVSystemAPIdotnet.Data
{
    public class TopCvDbContext : DbContext
    {
        public TopCvDbContext(DbContextOptions<TopCvDbContext> options) : base(options) { }
        public DbSet<User> User { get; set; }
        public DbSet<Article> Article { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<Job> Job { get; set; }
        public DbSet<JobDetails> JobDetail { get; set; }
        public DbSet<Applicant> Applicant { get; set; }
        public DbSet<Resume> Resume { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<CompanyInformationDetails> CompanyInformationDetail { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<Recruiter> Recruiter { get; set; }
        public DbSet<ApplicantJob> ApplicantJob { get; set; }
        public DbSet<MessageNotification> MessageNotification { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Image> Images { get; set; }

    }
}
