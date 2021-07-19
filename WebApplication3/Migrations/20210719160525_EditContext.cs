using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication3.Migrations
{
    public partial class EditContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BuyProducts_Products_ProductId",
                table: "BuyProducts");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "BuyProducts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "BuyProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_BuyProducts_Products_ProductId",
                table: "BuyProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BuyProducts_Products_ProductId",
                table: "BuyProducts");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "BuyProducts");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "BuyProducts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BuyProducts_Products_ProductId",
                table: "BuyProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
