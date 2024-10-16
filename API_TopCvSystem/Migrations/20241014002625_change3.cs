using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_TopCvSystem.Migrations
{
    /// <inheritdoc />
    public partial class change3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ID_SortOfUser",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ID_Message_Notification",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "Create_By",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "ID_Job_Details",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "ID_Company_Information_Details",
                table: "Company");

            migrationBuilder.AddColumn<int>(
                name: "ID_User",
                table: "Resume",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ID_Message",
                table: "MessageNotification",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ID_Job",
                table: "JobDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Working_Experience_Require",
                table: "Job",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "Application_Status",
                table: "Job",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "TaxID",
                table: "CompanyInformationDetail",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "ID_Company",
                table: "CompanyInformationDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ID_User",
                table: "Resume");

            migrationBuilder.DropColumn(
                name: "ID_Message",
                table: "MessageNotification");

            migrationBuilder.DropColumn(
                name: "ID_Job",
                table: "JobDetail");

            migrationBuilder.DropColumn(
                name: "ID_Company",
                table: "CompanyInformationDetail");

            migrationBuilder.AddColumn<int>(
                name: "ID_SortOfUser",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ID_Message_Notification",
                table: "Message",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Working_Experience_Require",
                table: "Job",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Application_Status",
                table: "Job",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<string>(
                name: "Create_By",
                table: "Job",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ID_Job_Details",
                table: "Job",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "TaxID",
                table: "CompanyInformationDetail",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ID_Company_Information_Details",
                table: "Company",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
