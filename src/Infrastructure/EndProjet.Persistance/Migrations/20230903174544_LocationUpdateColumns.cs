using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EndProjet.Persistance.Migrations
{
    public partial class LocationUpdateColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PickupLocations_CarReservations_CarReservationId",
                table: "PickupLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_ReturnLocations_CarReservations_CarReservationId",
                table: "ReturnLocations");

            migrationBuilder.DropIndex(
                name: "IX_ReturnLocations_CarReservationId",
                table: "ReturnLocations");

            migrationBuilder.DropIndex(
                name: "IX_PickupLocations_CarReservationId",
                table: "PickupLocations");

            migrationBuilder.AlterColumn<Guid>(
                name: "CarReservationId",
                table: "ReturnLocations",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "CarReservationId",
                table: "PickupLocations",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnLocations_CarReservationId",
                table: "ReturnLocations",
                column: "CarReservationId",
                unique: true,
                filter: "[CarReservationId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PickupLocations_CarReservationId",
                table: "PickupLocations",
                column: "CarReservationId",
                unique: true,
                filter: "[CarReservationId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_PickupLocations_CarReservations_CarReservationId",
                table: "PickupLocations",
                column: "CarReservationId",
                principalTable: "CarReservations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReturnLocations_CarReservations_CarReservationId",
                table: "ReturnLocations",
                column: "CarReservationId",
                principalTable: "CarReservations",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PickupLocations_CarReservations_CarReservationId",
                table: "PickupLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_ReturnLocations_CarReservations_CarReservationId",
                table: "ReturnLocations");

            migrationBuilder.DropIndex(
                name: "IX_ReturnLocations_CarReservationId",
                table: "ReturnLocations");

            migrationBuilder.DropIndex(
                name: "IX_PickupLocations_CarReservationId",
                table: "PickupLocations");

            migrationBuilder.AlterColumn<Guid>(
                name: "CarReservationId",
                table: "ReturnLocations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CarReservationId",
                table: "PickupLocations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReturnLocations_CarReservationId",
                table: "ReturnLocations",
                column: "CarReservationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PickupLocations_CarReservationId",
                table: "PickupLocations",
                column: "CarReservationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PickupLocations_CarReservations_CarReservationId",
                table: "PickupLocations",
                column: "CarReservationId",
                principalTable: "CarReservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReturnLocations_CarReservations_CarReservationId",
                table: "ReturnLocations",
                column: "CarReservationId",
                principalTable: "CarReservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
