using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorWebAppCamilla.Migrations
{
    /// <inheritdoc />
    public partial class DeleteFKUseraddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAddresses_AspNetUsers_ApplicationUserId",
                table: "UserAddresses");

            migrationBuilder.DropIndex(
                name: "IX_UserAddresses_ApplicationUserId",
                table: "UserAddresses");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "UserAddresses");

            migrationBuilder.AlterColumn<string>(
                name: "UserAddressId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserAddressId",
                table: "AspNetUsers",
                column: "UserAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserAddresses_UserAddressId",
                table: "AspNetUsers",
                column: "UserAddressId",
                principalTable: "UserAddresses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserAddresses_UserAddressId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserAddressId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "UserAddresses",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "UserAddressId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAddresses_ApplicationUserId",
                table: "UserAddresses",
                column: "ApplicationUserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAddresses_AspNetUsers_ApplicationUserId",
                table: "UserAddresses",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
