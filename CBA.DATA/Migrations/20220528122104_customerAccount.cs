using Microsoft.EntityFrameworkCore.Migrations;

namespace CBA.DATA.Migrations
{
    public partial class customerAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerLongID",
                table: "CustomerAccounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "CustomerAccounts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerLongID",
                table: "CustomerAccounts");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "CustomerAccounts");
        }
    }
}
