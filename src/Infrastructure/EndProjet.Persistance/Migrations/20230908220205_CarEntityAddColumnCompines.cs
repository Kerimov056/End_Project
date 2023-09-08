using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EndProjet.Persistance.Migrations
{
    public partial class CarEntityAddColumnCompines : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PickUpCampaigns",
                table: "Cars",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReturnCampaigns",
                table: "Cars",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isCampaigns",
                table: "Cars",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PickUpCampaigns",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "ReturnCampaigns",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "isCampaigns",
                table: "Cars");
        }
    }
}
