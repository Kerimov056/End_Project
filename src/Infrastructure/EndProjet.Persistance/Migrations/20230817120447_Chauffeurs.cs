using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EndProjet.Persistance.Migrations
{
    public partial class Chauffeurs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservationDate",
                table: "CarReservations");

            migrationBuilder.AlterColumn<Guid>(
                name: "ReturnLocationId",
                table: "CarReservations",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "PickupLocationId",
                table: "CarReservations",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "ChauffeursId",
                table: "CarReservations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Chauffeurs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    imagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chauffeurs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarReservations_ChauffeursId",
                table: "CarReservations",
                column: "ChauffeursId",
                unique: true,
                filter: "[ChauffeursId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_CarReservations_Chauffeurs_ChauffeursId",
                table: "CarReservations",
                column: "ChauffeursId",
                principalTable: "Chauffeurs",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarReservations_Chauffeurs_ChauffeursId",
                table: "CarReservations");

            migrationBuilder.DropTable(
                name: "Chauffeurs");

            migrationBuilder.DropIndex(
                name: "IX_CarReservations_ChauffeursId",
                table: "CarReservations");

            migrationBuilder.DropColumn(
                name: "ChauffeursId",
                table: "CarReservations");

            migrationBuilder.AlterColumn<Guid>(
                name: "ReturnLocationId",
                table: "CarReservations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PickupLocationId",
                table: "CarReservations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReservationDate",
                table: "CarReservations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
