using Microsoft.EntityFrameworkCore.Migrations;

namespace BotMonitor.Migrations
{
    public partial class AddCurrentZoneToBot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CurrentZone",
                table: "Bots",
                maxLength: 32,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentZone",
                table: "Bots");
        }
    }
}
