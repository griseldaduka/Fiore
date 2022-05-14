using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fiore.Migrations
{
    public partial class deleteColourEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Colours");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Colours",
                columns: table => new
                {
                    ColourId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Black = table.Column<bool>(type: "bit", nullable: false),
                    Blue = table.Column<bool>(type: "bit", nullable: false),
                    Gold = table.Column<bool>(type: "bit", nullable: false),
                    Green = table.Column<bool>(type: "bit", nullable: false),
                    Orange = table.Column<bool>(type: "bit", nullable: false),
                    Purple = table.Column<bool>(type: "bit", nullable: false),
                    Red = table.Column<bool>(type: "bit", nullable: false),
                    Rose = table.Column<bool>(type: "bit", nullable: false),
                    White = table.Column<bool>(type: "bit", nullable: false),
                    Yellow = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colours", x => x.ColourId);
                });
        }
    }
}
