using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_TopCvSystem.Migrations
{
    /// <inheritdoc />
    public partial class ff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID_Appicant",
                table: "ApplicantJob",
                newName: "ID_Applicant");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID_Applicant",
                table: "ApplicantJob",
                newName: "ID_Appicant");
        }
    }
}
