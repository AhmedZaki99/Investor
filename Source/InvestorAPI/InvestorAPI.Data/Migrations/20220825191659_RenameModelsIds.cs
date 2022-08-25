using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestorAPI.Data.Migrations
{
    public partial class RenameModelsIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VendorId",
                table: "Vendors",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UnitConversionId",
                table: "UnitConversions",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ScaleUnitId",
                table: "ScaleUnits",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Products",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "InvoiceId",
                table: "Invoices",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "InvoiceItemId",
                table: "InvoiceItems",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Customers",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ContactId",
                table: "Contacts",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Categories",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "BusinessTypeId",
                table: "BusinessTypes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "BusinessId",
                table: "Businesses",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "BillId",
                table: "Bills",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "BillItemId",
                table: "BillItems",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "Addresses",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Accounts",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Vendors",
                newName: "VendorId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UnitConversions",
                newName: "UnitConversionId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ScaleUnits",
                newName: "ScaleUnitId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Products",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Invoices",
                newName: "InvoiceId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "InvoiceItems",
                newName: "InvoiceItemId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Customers",
                newName: "CustomerId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Contacts",
                newName: "ContactId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Categories",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "BusinessTypes",
                newName: "BusinessTypeId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Businesses",
                newName: "BusinessId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Bills",
                newName: "BillId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "BillItems",
                newName: "BillItemId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Addresses",
                newName: "AddressId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Accounts",
                newName: "AccountId");
        }
    }
}
