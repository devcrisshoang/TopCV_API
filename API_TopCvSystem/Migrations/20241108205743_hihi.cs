﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_TopCvSystem.Migrations
{
    /// <inheritdoc />
    public partial class hihi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ID_Applicant_Job",
                table: "Resume");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ID_Applicant_Job",
                table: "Resume",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
