using Microsoft.EntityFrameworkCore.Migrations;

namespace E_commerce.Ef.Migrations
{
    public partial class AddedClumaninApplicatinUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Security",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Security",
                table: "Users");
        }
    }
}
