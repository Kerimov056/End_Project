using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EndProjet.Persistance.Migrations
{
    public partial class CampaignStatistika : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CampaignStatistikaId",
                table: "CarReservations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CampaignStatistikas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReservationSum = table.Column<int>(type: "int", nullable: false),
                    CampaignName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinshTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignStatistikas", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarReservations_CampaignStatistikaId",
                table: "CarReservations",
                column: "CampaignStatistikaId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarReservations_CampaignStatistikas_CampaignStatistikaId",
                table: "CarReservations",
                column: "CampaignStatistikaId",
                principalTable: "CampaignStatistikas",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarReservations_CampaignStatistikas_CampaignStatistikaId",
                table: "CarReservations");

            migrationBuilder.DropTable(
                name: "CampaignStatistikas");

            migrationBuilder.DropIndex(
                name: "IX_CarReservations_CampaignStatistikaId",
                table: "CarReservations");

            migrationBuilder.DropColumn(
                name: "CampaignStatistikaId",
                table: "CarReservations");
        }
    }
}
