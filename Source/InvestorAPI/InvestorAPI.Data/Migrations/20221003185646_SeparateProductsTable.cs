using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestorAPI.Data.Migrations
{
    public partial class SeparateProductsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Accounts_ExpenseAccountId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Accounts_IncomeAccountId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Accounts_InventoryAccountId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ScaleUnits_ScaleUnitId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ExpenseAccountId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_IncomeAccountId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_InventoryAccountId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ScaleUnitId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ExpenseAccountId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PurchaseDescription",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ReorderPoint",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SKU",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SalesDescription",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SalesPrice",
                table: "Products");
            
            migrationBuilder.DropColumn(
                name: "ScaleUnitId",
                table: "Products");
            
            migrationBuilder.DropColumn(
                name: "InventoryAccountId",
                table: "Products");
            
            migrationBuilder.DropColumn(
                name: "IncomeAccountId",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "SalesInformationId",
                table: "Products",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PurchasingInformationId",
                table: "Products",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InventoryDetailsId",
                table: "Products",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "InventoryInfos",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SKU = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    ReorderPoint = table.Column<double>(type: "float", nullable: true),
                    ScaleUnitId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    InventoryAccountId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryInfos_Accounts_InventoryAccountId",
                        column: x => x.InventoryAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InventoryInfos_ScaleUnits_ScaleUnitId",
                        column: x => x.ScaleUnitId,
                        principalTable: "ScaleUnits",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TradingInfos",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccountId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradingInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TradingInfos_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_InventoryDetailsId",
                table: "Products",
                column: "InventoryDetailsId",
                unique: true,
                filter: "[InventoryDetailsId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Products_PurchasingInformationId",
                table: "Products",
                column: "PurchasingInformationId",
                unique: true,
                filter: "[PurchasingInformationId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SalesInformationId",
                table: "Products",
                column: "SalesInformationId",
                unique: true,
                filter: "[SalesInformationId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryInfos_InventoryAccountId",
                table: "InventoryInfos",
                column: "InventoryAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryInfos_ScaleUnitId",
                table: "InventoryInfos",
                column: "ScaleUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_TradingInfos_AccountId",
                table: "TradingInfos",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_InventoryInfos_InventoryDetailsId",
                table: "Products",
                column: "InventoryDetailsId",
                principalTable: "InventoryInfos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_TradingInfos_PurchasingInformationId",
                table: "Products",
                column: "PurchasingInformationId",
                principalTable: "TradingInfos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_TradingInfos_SalesInformationId",
                table: "Products",
                column: "SalesInformationId",
                principalTable: "TradingInfos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_InventoryInfos_InventoryDetailsId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_TradingInfos_PurchasingInformationId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_TradingInfos_SalesInformationId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "InventoryInfos");

            migrationBuilder.DropTable(
                name: "TradingInfos");

            migrationBuilder.DropIndex(
                name: "IX_Products_InventoryDetailsId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_PurchasingInformationId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_SalesInformationId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SalesInformationId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PurchasingInformationId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "InventoryDetailsId",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "IncomeAccountId",
                table: "Products",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InventoryAccountId",
                table: "Products",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ScaleUnitId",
                table: "Products",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                table: "Products",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExpenseAccountId",
                table: "Products",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PurchaseDescription",
                table: "Products",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Quantity",
                table: "Products",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReorderPoint",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SKU",
                table: "Products",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SalesDescription",
                table: "Products",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SalesPrice",
                table: "Products",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ExpenseAccountId",
                table: "Products",
                column: "ExpenseAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_IncomeAccountId",
                table: "Products",
                column: "IncomeAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_InventoryAccountId",
                table: "Products",
                column: "InventoryAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ScaleUnitId",
                table: "Products",
                column: "ScaleUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Accounts_ExpenseAccountId",
                table: "Products",
                column: "ExpenseAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Accounts_IncomeAccountId",
                table: "Products",
                column: "IncomeAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Accounts_InventoryAccountId",
                table: "Products",
                column: "InventoryAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ScaleUnits_ScaleUnitId",
                table: "Products",
                column: "ScaleUnitId",
                principalTable: "ScaleUnits",
                principalColumn: "Id");
        }
    }
}
