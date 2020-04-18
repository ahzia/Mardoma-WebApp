using Microsoft.EntityFrameworkCore.Migrations;

namespace ArchiveSystem.Migrations
{
    public partial class lastChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "other",
                table: "Documents",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "other",
                table: "Documents");
        }
    }
}
