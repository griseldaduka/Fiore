using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fiore.Migrations
{
    public partial class AnotherColour : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Green",
                table: "Colours",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Green",
                table: "Colours");
        }
    }
}
