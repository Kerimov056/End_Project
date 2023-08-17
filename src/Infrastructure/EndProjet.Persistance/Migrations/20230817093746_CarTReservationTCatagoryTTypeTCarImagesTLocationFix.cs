using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EndProjet.Persistance.Migrations
{
    public partial class CarTReservationTCatagoryTTypeTCarImagesTLocationFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Test_Cars_CarId",
                table: "Test");

            migrationBuilder.DropForeignKey(
                name: "FK_Test_Tags_TagId",
                table: "Test");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Test",
                table: "Test");

            migrationBuilder.RenameTable(
                name: "Test",
                newName: "CarTags");

            migrationBuilder.RenameIndex(
                name: "IX_Test_TagId",
                table: "CarTags",
                newName: "IX_CarTags_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_Test_CarId",
                table: "CarTags",
                newName: "IX_CarTags_CarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarTags",
                table: "CarTags",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CarTags_Cars_CarId",
                table: "CarTags",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarTags_Tags_TagId",
                table: "CarTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarTags_Cars_CarId",
                table: "CarTags");

            migrationBuilder.DropForeignKey(
                name: "FK_CarTags_Tags_TagId",
                table: "CarTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarTags",
                table: "CarTags");

            migrationBuilder.RenameTable(
                name: "CarTags",
                newName: "Test");

            migrationBuilder.RenameIndex(
                name: "IX_CarTags_TagId",
                table: "Test",
                newName: "IX_Test_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_CarTags_CarId",
                table: "Test",
                newName: "IX_Test_CarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Test",
                table: "Test",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Test_Cars_CarId",
                table: "Test",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Test_Tags_TagId",
                table: "Test",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
