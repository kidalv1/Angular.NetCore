using Microsoft.EntityFrameworkCore.Migrations;

namespace DatingApp.API.Migrations
{
    public partial class Interests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Intrests",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Interests",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Interests",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Intrests",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
