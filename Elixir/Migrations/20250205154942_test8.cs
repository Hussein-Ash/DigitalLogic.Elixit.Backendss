using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Elixir.Migrations
{
    /// <inheritdoc />
    public partial class test8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("928f4275-38fb-4139-95c3-d70701582ab0"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "CreationDate", "Deleted", "Email", "Followings", "FullName", "Imgs", "Password", "PhoneNumber", "Role", "SavedProducts", "UserName" },
                values: new object[,]
                {
                    { new Guid("0fbcbed8-caf5-4fd7-b0e5-a1a3b3dc4359"), true, new DateTime(2025, 2, 5, 15, 49, 41, 620, DateTimeKind.Utc).AddTicks(4843), false, null, 0, "superadmin", new List<string>(), "$2a$11$GlZ7OQMS4jDOGkm0u9t8T.UycjRoGjS17fUwP01e8.chx4ZvX6Bv.", new List<string> { "01234567891" }, 3, 0, "superadmin" },
                    { new Guid("384275fe-b4e4-479c-853a-23266a035376"), true, new DateTime(2025, 2, 5, 15, 49, 41, 475, DateTimeKind.Utc).AddTicks(172), false, null, 0, "Admin", null, "$2a$11$pqyLBV5hq4pZE6i9niVpL.yMLJkjMktZHVY.75Lhy5T7ZgnAKCnjG", new List<string> { "01234567891" }, 3, 0, "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0fbcbed8-caf5-4fd7-b0e5-a1a3b3dc4359"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("384275fe-b4e4-479c-853a-23266a035376"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "CreationDate", "Deleted", "Email", "Followings", "FullName", "Imgs", "Password", "PhoneNumber", "Role", "SavedProducts", "UserName" },
                values: new object[] { new Guid("928f4275-38fb-4139-95c3-d70701582ab0"), true, new DateTime(2025, 2, 5, 15, 46, 11, 988, DateTimeKind.Utc).AddTicks(6994), false, null, 0, "Admin", null, "$2a$11$hRvtOnlow6I2VSUcSW5W2.F/oz0z5dOKJz3vSCMFxV/TShHyt4Vse", new List<string> { "01234567891" }, 3, 0, "Admin" });
        }
    }
}
