using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elixir.Migrations
{
    /// <inheritdoc />
    public partial class test4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("1ba8ef69-7e7b-4db3-ba4c-355db6bd9374"));

            migrationBuilder.AlterColumn<int>(
                name: "Role",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "CreationDate", "Deleted", "Email", "Followings", "FullName", "Imgs", "Password", "PhoneNumber", "Role", "SavedProducts", "UserName" },
                values: new object[] { new Guid("58db94b5-3613-45c5-82a9-0be2347a112b"), true, new DateTime(2025, 2, 4, 20, 28, 0, 865, DateTimeKind.Utc).AddTicks(5611), false, null, 0, "Admin", null, "$2a$11$/kOiVgAvYSwrdLSSdVwt..jPd4Sinsjx59ZxNKXKKfkMqZNjeMwUG", new List<string> { "01234567891" }, 3, 0, "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("58db94b5-3613-45c5-82a9-0be2347a112b"));

            migrationBuilder.AlterColumn<int>(
                name: "Role",
                table: "Users",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "CreationDate", "Deleted", "Email", "Followings", "FullName", "Imgs", "Password", "PhoneNumber", "Role", "SavedProducts", "UserName" },
                values: new object[] { new Guid("1ba8ef69-7e7b-4db3-ba4c-355db6bd9374"), false, new DateTime(2025, 2, 4, 18, 45, 32, 37, DateTimeKind.Utc).AddTicks(3194), false, null, 0, "Admin", null, "$2a$11$9Jwo.74siDJFF1gfLLshOeK3V0iXxPpEg4957zAF9jiYs14HVmJsG", new List<string> { "01234567891" }, 3, 0, "Admin" });
        }
    }
}
