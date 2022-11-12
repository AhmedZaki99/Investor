using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestorAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class ConvertTraderSubTablesToOwned : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Traders_Addresses_AddressId",
                table: "Traders");

            migrationBuilder.DropForeignKey(
                name: "FK_Traders_Contacts_ContactId",
                table: "Traders");

            migrationBuilder.DropIndex(
                name: "IX_Traders_AddressId",
                table: "Traders");

            migrationBuilder.DropIndex(
                name: "IX_Traders_ContactId",
                table: "Traders");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Traders");

            migrationBuilder.DropColumn(
                name: "ContactId",
                table: "Traders");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "Contacts");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Contacts",
                newName: "TraderId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Addresses",
                newName: "TraderId");

            migrationBuilder.AlterColumn<int>(
                name: "AccountScope",
                table: "Accounts",
                type: "int",
                nullable: false,
                computedColumnSql: "CASE\r\n    WHEN [BusinessId] IS NULL\r\n    THEN CASE\r\n        WHEN [BusinessTypeId] IS NULL\r\n        THEN CAST(2 AS INT)\r\n        ELSE CAST(3 AS INT)\r\n    END\r\n    ELSE CAST(1 AS INT)\r\nEND",
                oldClrType: typeof(int),
                oldType: "int",
                oldComputedColumnSql: "\r\n                        CASE\r\n                            WHEN [BusinessId] IS NULL\r\n                            THEN CASE\r\n		                        WHEN [BusinessTypeId] IS NULL\r\n		                        THEN CAST(2 AS INT)\r\n		                        ELSE CAST(3 AS INT)\r\n	                        END\r\n	                        ELSE CAST(1 AS INT)\r\n                        END\r\n                    ");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Traders_TraderId",
                table: "Addresses",
                column: "TraderId",
                principalTable: "Traders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Traders_TraderId",
                table: "Contacts",
                column: "TraderId",
                principalTable: "Traders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Traders_TraderId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Traders_TraderId",
                table: "Contacts");

            migrationBuilder.RenameColumn(
                name: "TraderId",
                table: "Contacts",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "TraderId",
                table: "Addresses",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "AddressId",
                table: "Traders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactId",
                table: "Traders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Contacts",
                type: "datetime2(3)",
                precision: 3,
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "Contacts",
                type: "datetime2(3)",
                precision: 3,
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AccountScope",
                table: "Accounts",
                type: "int",
                nullable: false,
                computedColumnSql: "\r\n                        CASE\r\n                            WHEN [BusinessId] IS NULL\r\n                            THEN CASE\r\n		                        WHEN [BusinessTypeId] IS NULL\r\n		                        THEN CAST(2 AS INT)\r\n		                        ELSE CAST(3 AS INT)\r\n	                        END\r\n	                        ELSE CAST(1 AS INT)\r\n                        END\r\n                    ",
                oldClrType: typeof(int),
                oldType: "int",
                oldComputedColumnSql: "CASE\r\n    WHEN [BusinessId] IS NULL\r\n    THEN CASE\r\n        WHEN [BusinessTypeId] IS NULL\r\n        THEN CAST(2 AS INT)\r\n        ELSE CAST(3 AS INT)\r\n    END\r\n    ELSE CAST(1 AS INT)\r\nEND");

            migrationBuilder.CreateIndex(
                name: "IX_Traders_AddressId",
                table: "Traders",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Traders_ContactId",
                table: "Traders",
                column: "ContactId");

            migrationBuilder.AddForeignKey(
                name: "FK_Traders_Addresses_AddressId",
                table: "Traders",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Traders_Contacts_ContactId",
                table: "Traders",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id");
        }
    }
}
