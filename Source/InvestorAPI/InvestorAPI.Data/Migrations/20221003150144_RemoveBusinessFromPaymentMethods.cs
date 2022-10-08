using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestorAPI.Data.Migrations
{
    public partial class RemoveBusinessFromPaymentMethods : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Addresses_BillingAddressId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Contacts_PrimaryContactId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentMethods_Businesses_BusinessId",
                table: "PaymentMethods");

            migrationBuilder.DropIndex(
                name: "IX_PaymentMethods_BusinessId",
                table: "PaymentMethods");

            migrationBuilder.DropColumn(
                name: "BusinessId",
                table: "PaymentMethods");

            migrationBuilder.RenameColumn(
                name: "PrimaryContactId",
                table: "Customers",
                newName: "ContactId");

            migrationBuilder.RenameColumn(
                name: "BillingAddressId",
                table: "Customers",
                newName: "AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_PrimaryContactId",
                table: "Customers",
                newName: "IX_Customers_ContactId");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_BillingAddressId",
                table: "Customers",
                newName: "IX_Customers_AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Addresses_AddressId",
                table: "Customers",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Contacts_ContactId",
                table: "Customers",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Addresses_AddressId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Contacts_ContactId",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "ContactId",
                table: "Customers",
                newName: "PrimaryContactId");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "Customers",
                newName: "BillingAddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_ContactId",
                table: "Customers",
                newName: "IX_Customers_PrimaryContactId");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_AddressId",
                table: "Customers",
                newName: "IX_Customers_BillingAddressId");

            migrationBuilder.AddColumn<string>(
                name: "BusinessId",
                table: "PaymentMethods",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethods_BusinessId",
                table: "PaymentMethods",
                column: "BusinessId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Addresses_BillingAddressId",
                table: "Customers",
                column: "BillingAddressId",
                principalTable: "Addresses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Contacts_PrimaryContactId",
                table: "Customers",
                column: "PrimaryContactId",
                principalTable: "Contacts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentMethods_Businesses_BusinessId",
                table: "PaymentMethods",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id");
        }
    }
}
