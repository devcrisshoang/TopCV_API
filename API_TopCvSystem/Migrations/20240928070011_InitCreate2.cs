using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_TopCvSystem.Migrations
{
    /// <inheritdoc />
    public partial class InitCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Notification_NotificationID",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_SortOfUser_SortOfUserID_SortOfUser",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_NotificationID",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_SortOfUserID_SortOfUser",
                table: "User");

            migrationBuilder.DropColumn(
                name: "NotificationID",
                table: "User");

            migrationBuilder.DropColumn(
                name: "SortOfUserID_SortOfUser",
                table: "User");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NotificationID",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SortOfUserID_SortOfUser",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_User_NotificationID",
                table: "User",
                column: "NotificationID");

            migrationBuilder.CreateIndex(
                name: "IX_User_SortOfUserID_SortOfUser",
                table: "User",
                column: "SortOfUserID_SortOfUser");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Notification_NotificationID",
                table: "User",
                column: "NotificationID",
                principalTable: "Notification",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_SortOfUser_SortOfUserID_SortOfUser",
                table: "User",
                column: "SortOfUserID_SortOfUser",
                principalTable: "SortOfUser",
                principalColumn: "ID_SortOfUser",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
