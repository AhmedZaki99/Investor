using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestorAPI.Data.Migrations
{
    public partial class CreateIndexesForProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ScaleUnits_Name",
                table: "ScaleUnits",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_Code",
                table: "Products",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Products_IsService",
                table: "Products",
                column: "IsService");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                table: "Products",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ScaleUnits_Name",
                table: "ScaleUnits");

            migrationBuilder.DropIndex(
                name: "IX_Products_Code",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_IsService",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_Name",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Categories_Name",
                table: "Categories");
        }
    }
}
