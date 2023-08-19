using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EndProjet.Persistance.Migrations
{
    public partial class CarReservationAddColumnEmailAndNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "CarReservations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "CarReservations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "CarReservations");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "CarReservations");
        }
    }
}
