using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EndProjet.Persistance.Migrations
{
    public partial class fixedAppUserColumnAddLikes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "Likes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Likes_AppUserId1",
                table: "Likes",
                column: "AppUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_AspNetUsers_AppUserId1",
                table: "Likes",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_AspNetUsers_AppUserId1",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Likes_AppUserId1",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "Likes");
        }
    }
}
