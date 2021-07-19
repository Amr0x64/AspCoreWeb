using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication3.Migrations
{
    public partial class EditBuyProduc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "BuyProducts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BuyProducts_ProductId",
                table: "BuyProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_BuyProducts_UserId",
                table: "BuyProducts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BuyProducts_AspNetUsers_UserId",
                table: "BuyProducts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BuyProducts_Products_ProductId",
                table: "BuyProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BuyProducts_AspNetUsers_UserId",
                table: "BuyProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_BuyProducts_Products_ProductId",
                table: "BuyProducts");

            migrationBuilder.DropIndex(
                name: "IX_BuyProducts_ProductId",
                table: "BuyProducts");

            migrationBuilder.DropIndex(
                name: "IX_BuyProducts_UserId",
                table: "BuyProducts");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "BuyProducts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
