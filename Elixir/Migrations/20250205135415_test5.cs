using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elixir.Migrations
{
    /// <inheritdoc />
    public partial class test5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stores_Wallets_WalletId",
                table: "Stores");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("58db94b5-3613-45c5-82a9-0be2347a112b"));

            migrationBuilder.AlterColumn<Guid>(
                name: "WalletId",
                table: "Stores",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "CreationDate", "Deleted", "Email", "Followings", "FullName", "Imgs", "Password", "PhoneNumber", "Role", "SavedProducts", "UserName" },
                values: new object[] { new Guid("4f8ba28e-6097-4906-928f-4d4ffb48b377"), true, new DateTime(2025, 2, 5, 13, 54, 15, 500, DateTimeKind.Utc).AddTicks(8759), false, null, 0, "Admin", null, "$2a$11$XdFacoQ6HPt2zkxq1EynzeYoqsJKgkEVj/yEYMPH8pqlfscxc0Ycy", new List<string> { "01234567891" }, 3, 0, "Admin" });

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_Wallets_WalletId",
                table: "Stores",
                column: "WalletId",
                principalTable: "Wallets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stores_Wallets_WalletId",
                table: "Stores");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("4f8ba28e-6097-4906-928f-4d4ffb48b377"));

            migrationBuilder.AlterColumn<Guid>(
                name: "WalletId",
                table: "Stores",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "CreationDate", "Deleted", "Email", "Followings", "FullName", "Imgs", "Password", "PhoneNumber", "Role", "SavedProducts", "UserName" },
                values: new object[] { new Guid("58db94b5-3613-45c5-82a9-0be2347a112b"), true, new DateTime(2025, 2, 4, 20, 28, 0, 865, DateTimeKind.Utc).AddTicks(5611), false, null, 0, "Admin", null, "$2a$11$/kOiVgAvYSwrdLSSdVwt..jPd4Sinsjx59ZxNKXKKfkMqZNjeMwUG", new List<string> { "01234567891" }, 3, 0, "Admin" });

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_Wallets_WalletId",
                table: "Stores",
                column: "WalletId",
                principalTable: "Wallets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
