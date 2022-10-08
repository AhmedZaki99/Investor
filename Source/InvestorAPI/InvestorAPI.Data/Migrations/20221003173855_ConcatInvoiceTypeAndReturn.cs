using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestorAPI.Data.Migrations
{
    public partial class ConcatInvoiceTypeAndReturn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Items",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(19,4)",
                oldPrecision: 19,
                oldScale: 4,
                oldComputedColumnSql: "[Quantity] * [Price]");


            migrationBuilder.DropIndex(
                name: "IX_Invoices_InvoiceType_IsReturn_TraderId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "IsReturn",
                table: "Invoices");

            migrationBuilder.AlterColumn<double>(
                name: "Quantity",
                table: "Items",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_InvoiceType_TraderId",
                table: "Invoices",
                columns: new[] { "InvoiceType", "TraderId" });


            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Items",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: false,
                computedColumnSql: "[Quantity] * [Price]",
                oldClrType: typeof(decimal),
                oldType: "decimal(19,4)",
                oldPrecision: 19,
                oldScale: 4);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Items",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(19,4)",
                oldPrecision: 19,
                oldScale: 4,
                oldComputedColumnSql: "[Quantity] * [Price]");


            migrationBuilder.DropIndex(
                name: "IX_Invoices_InvoiceType_TraderId",
                table: "Invoices");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Items",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<bool>(
                name: "IsReturn",
                table: "Invoices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_InvoiceType_IsReturn_TraderId",
                table: "Invoices",
                columns: new[] { "InvoiceType", "IsReturn", "TraderId" });


            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Items",
                type: "decimal(19,4)",
                precision: 19,
                scale: 4,
                nullable: false,
                computedColumnSql: "[Quantity] * [Price]",
                oldClrType: typeof(decimal),
                oldType: "decimal(19,4)",
                oldPrecision: 19,
                oldScale: 4);
        }
    }
}
