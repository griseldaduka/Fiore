using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fiore.Migrations
{
    public partial class AddColourEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ColourId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Colours",
                columns: table => new
                {
                    ColourId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    White = table.Column<bool>(type: "bit", nullable: false),
                    Red = table.Column<bool>(type: "bit", nullable: false),
                    Yellow = table.Column<bool>(type: "bit", nullable: false),
                    Blue = table.Column<bool>(type: "bit", nullable: false),
                    Purple = table.Column<bool>(type: "bit", nullable: false),
                    Orange = table.Column<bool>(type: "bit", nullable: false),
                    Rose = table.Column<bool>(type: "bit", nullable: false),
                    Black = table.Column<bool>(type: "bit", nullable: false),
                    Gold = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colours", x => x.ColourId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ColourId",
                table: "Products",
                column: "ColourId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Colours_ColourId",
                table: "Products",
                column: "ColourId",
                principalTable: "Colours",
                principalColumn: "ColourId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Colours_ColourId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Colours");

            migrationBuilder.DropIndex(
                name: "IX_Products_ColourId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ColourId",
                table: "Products");
        }
    }
}
