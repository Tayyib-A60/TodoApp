using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class todouser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserID",
                table: "Todos",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Todos_Users_UserID",
                table: "Todos");

            migrationBuilder.DropIndex(
                name: "IX_Todos_UserID",
                table: "Todos");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Todos");
        }
    }
}
