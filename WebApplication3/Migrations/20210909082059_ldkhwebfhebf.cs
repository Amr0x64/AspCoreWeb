using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication3.Migrations
{
    public partial class ldkhwebfhebf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_StreetNumbers_FiasStatmentfias_statements_id",
                table: "StreetNumbers",
                column: "FiasStatmentfias_statements_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StreetNumbers_FiasStatmentfias_statements_id",
                table: "StreetNumbers");
        }
    }
}
