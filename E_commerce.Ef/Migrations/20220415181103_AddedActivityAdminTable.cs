using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace E_commerce.Ef.Migrations
{
    public partial class AddedActivityAdminTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdminProperties_Users_UserId",
                table: "AdminProperties");

            migrationBuilder.DropIndex(
                name: "IX_AdminProperties_UserId",
                table: "AdminProperties");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AdminProperties",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "AdminActivity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypeActive = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdminId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminActivity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdminActivity_AdminProperties_AdminId",
                        column: x => x.AdminId,
                        principalTable: "AdminProperties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdminProperties_UserId",
                table: "AdminProperties",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AdminActivity_AdminId",
                table: "AdminActivity",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdminProperties_Users_UserId",
                table: "AdminProperties",
                column: "UserId",
                principalSchema: "Security",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdminProperties_Users_UserId",
                table: "AdminProperties");

            migrationBuilder.DropTable(
                name: "AdminActivity");

            migrationBuilder.DropIndex(
                name: "IX_AdminProperties_UserId",
                table: "AdminProperties");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AdminProperties",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_AdminProperties_UserId",
                table: "AdminProperties",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AdminProperties_Users_UserId",
                table: "AdminProperties",
                column: "UserId",
                principalSchema: "Security",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
