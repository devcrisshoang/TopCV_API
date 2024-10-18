using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_TopCvSystem.Migrations
{
    /// <inheritdoc />
    public partial class Init8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Admin",
                table: "SortOfUser");

            migrationBuilder.RenameColumn(
                name: "Recruiter",
                table: "SortOfUser",
                newName: "ID_Recruiter");

            migrationBuilder.RenameColumn(
                name: "Applicant",
                table: "SortOfUser",
                newName: "ID_Applicant");

            migrationBuilder.RenameColumn(
                name: "ID_User",
                table: "Resume",
                newName: "ID_Applicant_Job");

            migrationBuilder.AddColumn<int>(
                name: "ID_Resume",
                table: "ApplicantJob",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ID_Resume",
                table: "ApplicantJob");

            migrationBuilder.RenameColumn(
                name: "ID_Recruiter",
                table: "SortOfUser",
                newName: "Recruiter");

            migrationBuilder.RenameColumn(
                name: "ID_Applicant",
                table: "SortOfUser",
                newName: "Applicant");

            migrationBuilder.RenameColumn(
                name: "ID_Applicant_Job",
                table: "Resume",
                newName: "ID_User");

            migrationBuilder.AddColumn<int>(
                name: "Admin",
                table: "SortOfUser",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
