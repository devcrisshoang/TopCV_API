using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_TopCvSystem.Migrations
{
    /// <inheritdoc />
    public partial class x : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Create_Time",
                table: "Job",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Application_Date",
                table: "Job",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<bool>(
                name: "isRejected",
                table: "ApplicantJob",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isRejected",
                table: "ApplicantJob");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Create_Time",
                table: "Job",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Application_Date",
                table: "Job",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
