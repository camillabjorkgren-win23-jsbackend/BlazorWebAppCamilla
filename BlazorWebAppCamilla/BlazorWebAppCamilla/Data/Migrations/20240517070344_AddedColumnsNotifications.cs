using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorWebAppCamilla.Migrations
{
    /// <inheritdoc />
    public partial class AddedColumnsNotifications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DarkMode",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PreferredEmail",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SubscribeNewsletter",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DarkMode",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PreferredEmail",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SubscribeNewsletter",
                table: "AspNetUsers");
        }
    }
}
