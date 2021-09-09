using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication3.Migrations
{
    public partial class fbrnewbrfwef : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                 name: "FiasStatmentfias_statements_id",
                 table: "StreetNumbers");

            migrationBuilder.DropIndex(
                name: "IX_StreetNumbers_FiasStatmentfias_statements_id",
                table: "StreetNumbers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "StreetNumbers");
        }
    }
}
