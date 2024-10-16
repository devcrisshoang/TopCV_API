using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_TopCvSystem.Migrations
{
    /// <inheritdoc />
    public partial class Db : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ID_Resume",
                table: "Applicant");

            migrationBuilder.AddColumn<int>(
                name: "ID_Applicant",
                table: "Resume",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ID_Applicant",
                table: "Resume");

            migrationBuilder.AddColumn<int>(
                name: "ID_Resume",
                table: "Applicant",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
