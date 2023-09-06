using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EndProjet.Persistance.Migrations
{
    public partial class CarCommentAddColumnUserName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "CarComments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "CarComments");
        }
    }
}
