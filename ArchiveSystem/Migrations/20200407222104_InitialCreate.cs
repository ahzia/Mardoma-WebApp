using Microsoft.EntityFrameworkCore.Migrations;

namespace ArchiveSystem.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "Documents",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "fileName",
                table: "Documents",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Region",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "fileName",
                table: "Documents");
        }
    }
}
