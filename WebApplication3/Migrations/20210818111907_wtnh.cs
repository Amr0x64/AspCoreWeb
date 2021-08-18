using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication3.Migrations
{
    public partial class wtnh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartLine_Orders_OrderId",
                table: "CartLine");

            migrationBuilder.DropForeignKey(
                name: "FK_CartLine_Products_ProductId",
                table: "CartLine");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartLine",
                table: "CartLine");

            migrationBuilder.RenameTable(
                name: "CartLine",
                newName: "CartLines");

            migrationBuilder.RenameIndex(
                name: "IX_CartLine_ProductId",
                table: "CartLines",
                newName: "IX_CartLines_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_CartLine_OrderId",
                table: "CartLines",
                newName: "IX_CartLines_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartLines",
                table: "CartLines",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CartLines_Orders_OrderId",
                table: "CartLines",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CartLines_Products_ProductId",
                table: "CartLines",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartLines_Orders_OrderId",
                table: "CartLines");

            migrationBuilder.DropForeignKey(
                name: "FK_CartLines_Products_ProductId",
                table: "CartLines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartLines",
                table: "CartLines");

            migrationBuilder.RenameTable(
                name: "CartLines",
                newName: "CartLine");

            migrationBuilder.RenameIndex(
                name: "IX_CartLines_ProductId",
                table: "CartLine",
                newName: "IX_CartLine_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_CartLines_OrderId",
                table: "CartLine",
                newName: "IX_CartLine_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartLine",
                table: "CartLine",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CartLine_Orders_OrderId",
                table: "CartLine",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CartLine_Products_ProductId",
                table: "CartLine",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
