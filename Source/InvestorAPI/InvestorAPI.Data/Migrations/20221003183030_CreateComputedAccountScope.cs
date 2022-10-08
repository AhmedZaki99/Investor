using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestorAPI.Data.Migrations
{
    public partial class CreateComputedAccountScope : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountScope",
                table: "Accounts",
                type: "int",
                nullable: false,
                computedColumnSql: "\r\n                        CASE\r\n                            WHEN [BusinessId] IS NULL\r\n                            THEN CASE\r\n		                        WHEN [BusinessTypeId] IS NULL\r\n		                        THEN CAST(2 AS INT)\r\n		                        ELSE CAST(3 AS INT)\r\n	                        END\r\n	                        ELSE CAST(1 AS INT)\r\n                        END\r\n                    ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountScope",
                table: "Accounts");
        }
    }
}
