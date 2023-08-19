using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EndProjet.Persistance.Migrations
{
    public partial class otherCarReservationFixDateTimeAge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OtherCarReservation_AspNetUsers_AppUserId",
                table: "OtherCarReservation");

            migrationBuilder.DropForeignKey(
                name: "FK_OtherCarReservation_Cars_CarId",
                table: "OtherCarReservation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OtherCarReservation",
                table: "OtherCarReservation");

            migrationBuilder.DropColumn(
                name: "DTime",
                table: "OtherCarReservation");

            migrationBuilder.RenameTable(
                name: "OtherCarReservation",
                newName: "OtherCarReservations");

            migrationBuilder.RenameIndex(
                name: "IX_OtherCarReservation_CarId",
                table: "OtherCarReservations",
                newName: "IX_OtherCarReservations_CarId");

            migrationBuilder.RenameIndex(
                name: "IX_OtherCarReservation_AppUserId",
                table: "OtherCarReservations",
                newName: "IX_OtherCarReservations_AppUserId");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfYear",
                table: "OtherCarReservations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_OtherCarReservations",
                table: "OtherCarReservations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OtherCarReservations_AspNetUsers_AppUserId",
                table: "OtherCarReservations",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OtherCarReservations_Cars_CarId",
                table: "OtherCarReservations",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OtherCarReservations_AspNetUsers_AppUserId",
                table: "OtherCarReservations");

            migrationBuilder.DropForeignKey(
                name: "FK_OtherCarReservations_Cars_CarId",
                table: "OtherCarReservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OtherCarReservations",
                table: "OtherCarReservations");

            migrationBuilder.DropColumn(
                name: "DateOfYear",
                table: "OtherCarReservations");

            migrationBuilder.RenameTable(
                name: "OtherCarReservations",
                newName: "OtherCarReservation");

            migrationBuilder.RenameIndex(
                name: "IX_OtherCarReservations_CarId",
                table: "OtherCarReservation",
                newName: "IX_OtherCarReservation_CarId");

            migrationBuilder.RenameIndex(
                name: "IX_OtherCarReservations_AppUserId",
                table: "OtherCarReservation",
                newName: "IX_OtherCarReservation_AppUserId");

            migrationBuilder.AddColumn<int>(
                name: "DTime",
                table: "OtherCarReservation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OtherCarReservation",
                table: "OtherCarReservation",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OtherCarReservation_AspNetUsers_AppUserId",
                table: "OtherCarReservation",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OtherCarReservation_Cars_CarId",
                table: "OtherCarReservation",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
