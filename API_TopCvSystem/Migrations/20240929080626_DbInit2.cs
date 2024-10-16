using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_TopCvSystem.Migrations
{
    /// <inheritdoc />
    public partial class DbInit2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobApplication");

            migrationBuilder.DropColumn(
                name: "ID_Job",
                table: "Recruiter");

            migrationBuilder.RenameColumn(
                name: "ID_Application",
                table: "Applicant_Application",
                newName: "ID_Job");

            migrationBuilder.AddColumn<DateTime>(
                name: "Application_Date",
                table: "Job",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Application_Status",
                table: "Job",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ID_Recruiter",
                table: "Job",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Application_Date",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "Application_Status",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "ID_Recruiter",
                table: "Job");

            migrationBuilder.RenameColumn(
                name: "ID_Job",
                table: "Applicant_Application",
                newName: "ID_Application");

            migrationBuilder.AddColumn<int>(
                name: "ID_Job",
                table: "Recruiter",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "JobApplication",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Application_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Application_Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ID_Applicant = table.Column<int>(type: "int", nullable: false),
                    ID_Job = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobApplication", x => x.ID);
                });
        }
    }
}
