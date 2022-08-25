using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestorAPI.Data.Migrations
{
    public partial class RenameUnitConversionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnitConversion_ScaleUnits_SourceUnitId",
                table: "UnitConversion");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitConversion_ScaleUnits_TargetUnitId",
                table: "UnitConversion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UnitConversion",
                table: "UnitConversion");

            migrationBuilder.RenameTable(
                name: "UnitConversion",
                newName: "UnitConversions");

            migrationBuilder.RenameIndex(
                name: "IX_UnitConversion_TargetUnitId",
                table: "UnitConversions",
                newName: "IX_UnitConversions_TargetUnitId");

            migrationBuilder.RenameIndex(
                name: "IX_UnitConversion_SourceUnitId",
                table: "UnitConversions",
                newName: "IX_UnitConversions_SourceUnitId");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Products",
                type: "nvarchar(63)",
                maxLength: 63,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UnitConversions",
                table: "UnitConversions",
                column: "UnitConversionId");

            migrationBuilder.AddForeignKey(
                name: "FK_UnitConversions_ScaleUnits_SourceUnitId",
                table: "UnitConversions",
                column: "SourceUnitId",
                principalTable: "ScaleUnits",
                principalColumn: "ScaleUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_UnitConversions_ScaleUnits_TargetUnitId",
                table: "UnitConversions",
                column: "TargetUnitId",
                principalTable: "ScaleUnits",
                principalColumn: "ScaleUnitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnitConversions_ScaleUnits_SourceUnitId",
                table: "UnitConversions");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitConversions_ScaleUnits_TargetUnitId",
                table: "UnitConversions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UnitConversions",
                table: "UnitConversions");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "UnitConversions",
                newName: "UnitConversion");

            migrationBuilder.RenameIndex(
                name: "IX_UnitConversions_TargetUnitId",
                table: "UnitConversion",
                newName: "IX_UnitConversion_TargetUnitId");

            migrationBuilder.RenameIndex(
                name: "IX_UnitConversions_SourceUnitId",
                table: "UnitConversion",
                newName: "IX_UnitConversion_SourceUnitId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UnitConversion",
                table: "UnitConversion",
                column: "UnitConversionId");

            migrationBuilder.AddForeignKey(
                name: "FK_UnitConversion_ScaleUnits_SourceUnitId",
                table: "UnitConversion",
                column: "SourceUnitId",
                principalTable: "ScaleUnits",
                principalColumn: "ScaleUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_UnitConversion_ScaleUnits_TargetUnitId",
                table: "UnitConversion",
                column: "TargetUnitId",
                principalTable: "ScaleUnits",
                principalColumn: "ScaleUnitId");
        }
    }
}
