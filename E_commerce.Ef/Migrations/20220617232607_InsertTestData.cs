using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace E_commerce.Ef.Migrations
{
    public partial class InsertTestData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                               table: "Roles",
                               columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                               values: new object[] { Guid.NewGuid().ToString(), "Admin", "Admin".ToUpper(), Guid.NewGuid().ToString() },
                               schema: "Security"
                           );

            migrationBuilder.InsertData(
                               table: "Roles",
                               columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                               values: new object[] { Guid.NewGuid().ToString(), "User", "User".ToUpper(), Guid.NewGuid().ToString() },
                               schema: "Security"
                           );

            migrationBuilder.InsertData(
                               table: "Country",
                               columns: new[] { "Id", "Name" },
                               values: new object[] { Guid.NewGuid().ToString(), "Egypt" }
                           );

            migrationBuilder.InsertData(
                   table: "Country",
                   columns: new[] { "Id", "Name" },
                   values: new object[] { Guid.NewGuid().ToString(), "India" }
               );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
