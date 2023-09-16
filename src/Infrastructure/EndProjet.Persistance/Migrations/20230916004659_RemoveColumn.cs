using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EndProjet.Persistance.Migrations
{
    public partial class RemoveColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarReservations_CampaignStatistikas_CampaignStatistikaId",
                table: "CarReservations");

            migrationBuilder.DropIndex(
                name: "IX_CarReservations_CampaignStatistikaId",
                table: "CarReservations");

            migrationBuilder.DropColumn(
                name: "CampaignStatistikaId",
                table: "CarReservations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CampaignStatistikaId",
                table: "CarReservations",
                type: "uniqueidentifier",
                nullable: true);

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
    }
}
