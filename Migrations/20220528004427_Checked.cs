using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fiore.Migrations
{
    public partial class Checked : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Checked",
                table: "Complaints",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Checked",
                table: "Complaints");
        }
    }
}
