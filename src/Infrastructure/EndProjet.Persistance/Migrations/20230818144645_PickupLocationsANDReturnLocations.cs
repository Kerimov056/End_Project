using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EndProjet.Persistance.Migrations
{
    public partial class PickupLocationsANDReturnLocations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarReservations_Locations_PickupLocationId",
                table: "CarReservations");

            migrationBuilder.DropForeignKey(
                name: "FK_CarReservations_Locations_ReturnLocationId",
                table: "CarReservations");

            migrationBuilder.DropIndex(
                name: "IX_CarReservations_PickupLocationId",
                table: "CarReservations");

            migrationBuilder.DropIndex(
                name: "IX_CarReservations_ReturnLocationId",
                table: "CarReservations");

            migrationBuilder.DropColumn(
                name: "PickupLocationId",
                table: "CarReservations");

            migrationBuilder.DropColumn(
                name: "ReturnLocationId",
                table: "CarReservations");

            migrationBuilder.AddColumn<Guid>(
                name: "CarReservationId",
                table: "Locations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_CarReservationId",
                table: "Locations",
                column: "CarReservationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_CarReservations_CarReservationId",
                table: "Locations",
                column: "CarReservationId",
                principalTable: "CarReservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_CarReservations_CarReservationId",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Locations_CarReservationId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "CarReservationId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Locations");

            migrationBuilder.AddColumn<Guid>(
                name: "PickupLocationId",
                table: "CarReservations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ReturnLocationId",
                table: "CarReservations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CarReservations_PickupLocationId",
                table: "CarReservations",
                column: "PickupLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_CarReservations_ReturnLocationId",
                table: "CarReservations",
                column: "ReturnLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarReservations_Locations_PickupLocationId",
                table: "CarReservations",
                column: "PickupLocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CarReservations_Locations_ReturnLocationId",
                table: "CarReservations",
                column: "ReturnLocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
