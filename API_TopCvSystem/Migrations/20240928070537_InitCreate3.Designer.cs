﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TopCVSystemAPIdotnet.Data;

#nullable disable

namespace API_TopCvSystem.Migrations
{
    [DbContext(typeof(TopCvDbContext))]
    [Migration("20240928070537_InitCreate3")]
    partial class InitCreate3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TopCVSystemAPIdotnet.Data.Entities.Admin", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone_Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Admin");
                });

            modelBuilder.Entity("TopCVSystemAPIdotnet.Data.Entities.Applicant", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Applicant_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ID_Resume")
                        .HasColumnType("int");

                    b.Property<string>("Job_Desire")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone_Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Working_Experience")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Working_Location_Desire")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Applicant");
                });

            modelBuilder.Entity("TopCVSystemAPIdotnet.Data.Entities.Applicant_Application", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("ID_Appicant")
                        .HasColumnType("int");

                    b.Property<int>("ID_Application")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Applicant_Application");
                });

            modelBuilder.Entity("TopCVSystemAPIdotnet.Data.Entities.Article", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Article_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Create_By")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Create_Time")
                        .HasColumnType("datetime2");

                    b.Property<string>("Delete_By")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Article");
                });

            modelBuilder.Entity("TopCVSystemAPIdotnet.Data.Entities.Company", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Company_Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Company_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Field")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Green_Badge")
                        .HasColumnType("bit");

                    b.Property<string>("Hotline")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ID_Company_Information_Details")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isCheck")
                        .HasColumnType("bit");

                    b.HasKey("ID");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("TopCVSystemAPIdotnet.Data.Entities.CompanyInformationDetails", b =>
                {
                    b.Property<int>("ID_Company_Information_Details")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID_Company_Information_Details"));

                    b.Property<DateTime>("DateFounded")
                        .HasColumnType("datetime2");

                    b.Property<string>("TaxID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Website")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID_Company_Information_Details");

                    b.ToTable("CompanyInformationDetail");
                });

            modelBuilder.Entity("TopCVSystemAPIdotnet.Data.Entities.Job", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Create_By")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Create_Time")
                        .HasColumnType("datetime2");

                    b.Property<int>("ID_Job_Details")
                        .HasColumnType("int");

                    b.Property<string>("Job_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Working_Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Working_Experience_Require")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Job");
                });

            modelBuilder.Entity("TopCVSystemAPIdotnet.Data.Entities.JobApplication", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("Application_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Application_Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ID_Applicant")
                        .HasColumnType("int");

                    b.Property<int>("ID_Job")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("JobApplication");
                });

            modelBuilder.Entity("TopCVSystemAPIdotnet.Data.Entities.JobDetails", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Benefit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender_Require")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Job_Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Skill_Require")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Working_Method")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Working_Position")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Working_Time")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("JobDetail");
                });

            modelBuilder.Entity("TopCVSystemAPIdotnet.Data.Entities.Message", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ID_Message_Notification")
                        .HasColumnType("int");

                    b.Property<int>("Receiver_ID")
                        .HasColumnType("int");

                    b.Property<int>("Sender_ID")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("TopCVSystemAPIdotnet.Data.Entities.MessageNotification", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.HasKey("ID");

                    b.ToTable("MessageNotification");
                });

            modelBuilder.Entity("TopCVSystemAPIdotnet.Data.Entities.Notification", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ID_User")
                        .HasColumnType("int");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("Notification");
                });

            modelBuilder.Entity("TopCVSystemAPIdotnet.Data.Entities.Recruiter", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Email_Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ID_Article")
                        .HasColumnType("int");

                    b.Property<int>("ID_Company")
                        .HasColumnType("int");

                    b.Property<int>("ID_Job")
                        .HasColumnType("int");

                    b.Property<string>("Phone_Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Recruiter_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Recruiter");
                });

            modelBuilder.Entity("TopCVSystemAPIdotnet.Data.Entities.Resume", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Applicant_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Certificate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Create_By")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Delete_By")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Education")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Introduction")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Job_Applying")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone_Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Skills")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Resume");
                });

            modelBuilder.Entity("TopCVSystemAPIdotnet.Data.Entities.SortOfUser", b =>
                {
                    b.Property<int>("ID_SortOfUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID_SortOfUser"));

                    b.Property<string>("Admin")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Applicant")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Recruiter")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID_SortOfUser");

                    b.ToTable("SortOfUser");
                });

            modelBuilder.Entity("TopCVSystemAPIdotnet.Data.Entities.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("ID_Notification")
                        .HasColumnType("int");

                    b.Property<int>("ID_SortOfUser")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("User");
                });
#pragma warning restore 612, 618
        }
    }
}
