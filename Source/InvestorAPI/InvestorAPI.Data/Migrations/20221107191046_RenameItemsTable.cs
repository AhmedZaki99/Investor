using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestorAPI.Data.Migrations
{
    public partial class RenameItemsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                table: "Items",
                name: "IX_Items_InvoiceId",
                newName: "IX_InvoiceItems_InvoiceId");

            migrationBuilder.RenameIndex(
                table: "Items",
                name: "IX_Items_ProductId",
                newName: "IX_InvoiceItems_ProductId");

            migrationBuilder.RenameTable(
               name: "Items",
               newName: "InvoiceItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                table: "InvoiceItems",
                name: "IX_InvoiceItems_InvoiceId",
                newName: "IX_Items_InvoiceId");

            migrationBuilder.RenameIndex(
                table: "InvoiceItems",
                name: "IX_InvoiceItems_ProductId",
                newName: "IX_Items_ProductId");

            migrationBuilder.RenameTable(
                name: "InvoiceItems",
                newName: "Items");
        }
    }
}
