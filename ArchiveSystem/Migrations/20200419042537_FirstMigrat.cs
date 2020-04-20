using Microsoft.EntityFrameworkCore.Migrations;

namespace ArchiveSystem.Migrations
{
    public partial class FirstMigrat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Catagory",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "Grant",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "Region",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "SubCatagory",
                table: "Documents");

            migrationBuilder.AddColumn<string>(
                name: "Organization",
                table: "Documents",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Province",
                table: "Documents",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Topic",
                table: "Documents",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Where",
                table: "Documents",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Organization",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "Province",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "Topic",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "Where",
                table: "Documents");

            migrationBuilder.AddColumn<string>(
                name: "Catagory",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Grant",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubCatagory",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
