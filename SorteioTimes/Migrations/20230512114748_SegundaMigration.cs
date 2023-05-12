using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SorteioTimes.Migrations
{
    public partial class SegundaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Times");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Times",
                columns: table => new
                {
                    Nome = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                });
        }
    }
}
