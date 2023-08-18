using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EndProjet.Persistance.Migrations
{
    public partial class PickupLocationsANDReturnLocationsFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Locations_CarReservationId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Locations");

            migrationBuilder.CreateTable(
                name: "PickupLocations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    CarReservationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PickupLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PickupLocations_CarReservations_CarReservationId",
                        column: x => x.CarReservationId,
                        principalTable: "CarReservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReturnLocations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    CarReservationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReturnLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReturnLocations_CarReservations_CarReservationId",
                        column: x => x.CarReservationId,
                        principalTable: "CarReservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Locations_CarReservationId",
                table: "Locations",
                column: "CarReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_PickupLocations_CarReservationId",
                table: "PickupLocations",
                column: "CarReservationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReturnLocations_CarReservationId",
                table: "ReturnLocations",
                column: "CarReservationId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PickupLocations");

            migrationBuilder.DropTable(
                name: "ReturnLocations");

            migrationBuilder.DropIndex(
                name: "IX_Locations_CarReservationId",
                table: "Locations");

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
        }
    }
}
