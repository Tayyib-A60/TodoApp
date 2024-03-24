using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class usertodos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Todos_Users_UserID",
                table: "Todos");

            migrationBuilder.DropIndex(
                name: "IX_Todos_UserID",
                table: "Todos");

            migrationBuilder.AddColumn<Guid>(
                name: "UserEntityID",
                table: "Todos",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Todos_UserEntityID",
                table: "Todos",
                column: "UserEntityID");

            migrationBuilder.AddForeignKey(
                name: "FK_Todos_Users_UserEntityID",
                table: "Todos",
                column: "UserEntityID",
                principalTable: "Users",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Todos_Users_UserEntityID",
                table: "Todos");

            migrationBuilder.DropIndex(
                name: "IX_Todos_UserEntityID",
                table: "Todos");

            migrationBuilder.DropColumn(
                name: "UserEntityID",
                table: "Todos");

            migrationBuilder.CreateIndex(
                name: "IX_Todos_UserID",
                table: "Todos",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Todos_Users_UserID",
                table: "Todos",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
