using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestorAPI.Data.Migrations
{
    public partial class AddRulesToBusinessType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DisableProducts",
                table: "BusinessTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DisableServices",
                table: "BusinessTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NoInventory",
                table: "BusinessTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SalesOnly",
                table: "BusinessTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "BusinessTypeId",
                table: "Accounts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_BusinessTypeId",
                table: "Accounts",
                column: "BusinessTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_BusinessTypes_BusinessTypeId",
                table: "Accounts",
                column: "BusinessTypeId",
                principalTable: "BusinessTypes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_BusinessTypes_BusinessTypeId",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_BusinessTypeId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "DisableProducts",
                table: "BusinessTypes");

            migrationBuilder.DropColumn(
                name: "DisableServices",
                table: "BusinessTypes");

            migrationBuilder.DropColumn(
                name: "NoInventory",
                table: "BusinessTypes");

            migrationBuilder.DropColumn(
                name: "SalesOnly",
                table: "BusinessTypes");

            migrationBuilder.DropColumn(
                name: "BusinessTypeId",
                table: "Accounts");
        }
    }
}
