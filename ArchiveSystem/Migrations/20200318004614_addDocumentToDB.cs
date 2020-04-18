using Microsoft.EntityFrameworkCore.Migrations;

namespace ArchiveSystem.Migrations
{
    public partial class addDocumentToDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<int>(nullable: false),
                    Grant = table.Column<string>(nullable: false),
                    Catagory = table.Column<string>(nullable: false),
                    SubCatagory = table.Column<string>(nullable: false),
                    About = table.Column<string>(nullable: true),
                    file = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Documents");
        }
    }
}
