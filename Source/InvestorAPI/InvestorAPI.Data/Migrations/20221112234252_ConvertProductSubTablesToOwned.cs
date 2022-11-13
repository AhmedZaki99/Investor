using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestorAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class ConvertProductSubTablesToOwned : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "InventoryDetailsId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PurchasingInformationId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SalesInformationId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "InventoryInfos",
                newName: "ProductId");

            migrationBuilder.CreateTable(
                name: "PurchasingInfos",
                columns: table => new
                {
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccountId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchasingInfos", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_PurchasingInfos_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchasingInfos_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesInfos",
                columns: table => new
                {
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccountId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesInfos", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_SalesInfos_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SalesInfos_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PurchasingInfos_AccountId",
                table: "PurchasingInfos",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesInfos_AccountId",
                table: "SalesInfos",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryInfos_Products_ProductId",
                table: "InventoryInfos",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryInfos_Products_ProductId",
                table: "InventoryInfos");

            migrationBuilder.DropTable(
                name: "PurchasingInfos");

            migrationBuilder.DropTable(
                name: "SalesInfos");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "InventoryInfos",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "InventoryDetailsId",
                table: "Products",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PurchasingInformationId",
                table: "Products",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SalesInformationId",
                table: "Products",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TradingInfos",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccountId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false)
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
    }
}
