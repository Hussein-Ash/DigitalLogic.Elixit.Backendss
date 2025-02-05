using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elixir.Migrations
{
    /// <inheritdoc />
    public partial class test6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stores_Wallets_WalletId",
                table: "Stores");

            migrationBuilder.DropIndex(
                name: "IX_Wallets_StoreId",
                table: "Wallets");

            migrationBuilder.DropIndex(
                name: "IX_Stores_WalletId",
                table: "Stores");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("4f8ba28e-6097-4906-928f-4d4ffb48b377"));

            migrationBuilder.DropColumn(
                name: "WalletId",
                table: "Stores");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "CreationDate", "Deleted", "Email", "Followings", "FullName", "Imgs", "Password", "PhoneNumber", "Role", "SavedProducts", "UserName" },
                values: new object[] { new Guid("219e7de7-bb65-4f82-9721-4c07ed5e5f74"), true, new DateTime(2025, 2, 5, 13, 59, 12, 464, DateTimeKind.Utc).AddTicks(7486), false, null, 0, "Admin", null, "$2a$11$65tgrBpnW8fiE0gujiELgudEeWMeiw6qK031A/VoBgFDa/NehckwK", new List<string> { "01234567891" }, 3, 0, "Admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_StoreId",
                table: "Wallets",
                column: "StoreId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Wallets_StoreId",
                table: "Wallets");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("219e7de7-bb65-4f82-9721-4c07ed5e5f74"));

            migrationBuilder.AddColumn<Guid>(
                name: "WalletId",
                table: "Stores",
                type: "uuid",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "CreationDate", "Deleted", "Email", "Followings", "FullName", "Imgs", "Password", "PhoneNumber", "Role", "SavedProducts", "UserName" },
                values: new object[] { new Guid("4f8ba28e-6097-4906-928f-4d4ffb48b377"), true, new DateTime(2025, 2, 5, 13, 54, 15, 500, DateTimeKind.Utc).AddTicks(8759), false, null, 0, "Admin", null, "$2a$11$XdFacoQ6HPt2zkxq1EynzeYoqsJKgkEVj/yEYMPH8pqlfscxc0Ycy", new List<string> { "01234567891" }, 3, 0, "Admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_StoreId",
                table: "Wallets",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_WalletId",
                table: "Stores",
                column: "WalletId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_Wallets_WalletId",
                table: "Stores",
                column: "WalletId",
                principalTable: "Wallets",
                principalColumn: "Id");
        }
    }
}
