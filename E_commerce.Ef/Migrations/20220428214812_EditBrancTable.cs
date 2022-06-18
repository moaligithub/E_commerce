using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace E_commerce.Ef.Migrations
{
    public partial class EditBrancTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branch_City_CityId",
                table: "Branch");

            migrationBuilder.DropForeignKey(
                name: "FK_Branch_Country_CountryId",
                table: "Branch");

            migrationBuilder.DropIndex(
                name: "IX_Branch_CityId",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Branch");

            migrationBuilder.AlterColumn<Guid>(
                name: "CountryId",
                table: "Branch",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Branch_Country_CountryId",
                table: "Branch",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branch_Country_CountryId",
                table: "Branch");

            migrationBuilder.AlterColumn<Guid>(
                name: "CountryId",
                table: "Branch",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "CityId",
                table: "Branch",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Branch_CityId",
                table: "Branch",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Branch_City_CityId",
                table: "Branch",
                column: "CityId",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Branch_Country_CountryId",
                table: "Branch",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
