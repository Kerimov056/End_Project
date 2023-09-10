using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EndProjet.Persistance.Migrations
{
    public partial class AppUserAddColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ImagePath",
                table: "AspNetUsers",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "AspNetUsers");
        }
    }
}
