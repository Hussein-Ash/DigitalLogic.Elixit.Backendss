using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elixir.Migrations
{
    /// <inheritdoc />
    public partial class test7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("219e7de7-bb65-4f82-9721-4c07ed5e5f74"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "CreationDate", "Deleted", "Email", "Followings", "FullName", "Imgs", "Password", "PhoneNumber", "Role", "SavedProducts", "UserName" },
                values: new object[] { new Guid("928f4275-38fb-4139-95c3-d70701582ab0"), true, new DateTime(2025, 2, 5, 15, 46, 11, 988, DateTimeKind.Utc).AddTicks(6994), false, null, 0, "Admin", null, "$2a$11$hRvtOnlow6I2VSUcSW5W2.F/oz0z5dOKJz3vSCMFxV/TShHyt4Vse", new List<string> { "01234567891" }, 3, 0, "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("928f4275-38fb-4139-95c3-d70701582ab0"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "CreationDate", "Deleted", "Email", "Followings", "FullName", "Imgs", "Password", "PhoneNumber", "Role", "SavedProducts", "UserName" },
                values: new object[] { new Guid("219e7de7-bb65-4f82-9721-4c07ed5e5f74"), true, new DateTime(2025, 2, 5, 13, 59, 12, 464, DateTimeKind.Utc).AddTicks(7486), false, null, 0, "Admin", null, "$2a$11$65tgrBpnW8fiE0gujiELgudEeWMeiw6qK031A/VoBgFDa/NehckwK", new List<string> { "01234567891" }, 3, 0, "Admin" });
        }
    }
}
