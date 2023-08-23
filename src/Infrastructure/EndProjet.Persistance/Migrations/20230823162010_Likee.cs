﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EndProjet.Persistance.Migrations
{
    public partial class Likee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Likes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "CarCommentId",
                table: "Likes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CarCommentId1",
                table: "Likes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Likes_AppUserId",
                table: "Likes",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_CarCommentId",
                table: "Likes",
                column: "CarCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_CarCommentId1",
                table: "Likes",
                column: "CarCommentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_AspNetUsers_AppUserId",
                table: "Likes",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_CarComments_CarCommentId",
                table: "Likes",
                column: "CarCommentId",
                principalTable: "CarComments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_CarComments_CarCommentId1",
                table: "Likes",
                column: "CarCommentId1",
                principalTable: "CarComments",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_AspNetUsers_AppUserId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_CarComments_CarCommentId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_CarComments_CarCommentId1",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Likes_AppUserId",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Likes_CarCommentId",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Likes_CarCommentId1",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "CarCommentId",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "CarCommentId1",
                table: "Likes");
        }
    }
}
