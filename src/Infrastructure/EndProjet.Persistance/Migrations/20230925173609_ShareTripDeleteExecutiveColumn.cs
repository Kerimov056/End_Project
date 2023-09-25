using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EndProjet.Persistance.Migrations
{
    public partial class ShareTripDeleteExecutiveColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Executive",
                table: "ShareTrip");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Executive",
                table: "ShareTrip",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
