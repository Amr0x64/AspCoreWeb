using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication3.Migrations
{
    public partial class EditBuyProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdUser",
                table: "BuyProducts",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "IdProduct",
                table: "BuyProducts",
                newName: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "BuyProducts",
                newName: "IdUser");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "BuyProducts",
                newName: "IdProduct");
        }
    }
}
