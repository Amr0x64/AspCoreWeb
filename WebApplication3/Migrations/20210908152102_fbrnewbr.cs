using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication3.Migrations
{
    public partial class fbrnewbr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "StreetNumbers");

            migrationBuilder.DropIndex(
                name: "IX_StreetNumbers_FiasStatmentfias_statements_id",
                table: "StreetNumbers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "StreetNumbers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
