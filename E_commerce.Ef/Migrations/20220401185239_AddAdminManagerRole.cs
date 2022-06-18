using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace E_commerce.Ef.Migrations
{
    public partial class AddAdminManagerRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                    table : "Roles",
                    columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                    values: new object[] { Guid.NewGuid().ToString(), "AdminManager", "AdminManager".ToUpper(), Guid.NewGuid().ToString() },
                    schema: "Security"
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
