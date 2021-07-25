using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication3.Migrations
{
    public partial class AddOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BuyProducts_AspNetUsers_UserId",
                table: "BuyProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_BuyProducts_Products_ProductId",
                table: "BuyProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BuyProducts",
                table: "BuyProducts");

            migrationBuilder.RenameTable(
                name: "BuyProducts",
                newName: "BuyProduct");

            migrationBuilder.RenameIndex(
                name: "IX_BuyProducts_UserId",
                table: "BuyProduct",
                newName: "IX_BuyProduct_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_BuyProducts_ProductId",
                table: "BuyProduct",
                newName: "IX_BuyProduct_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BuyProduct",
                table: "BuyProduct",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Line1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Line2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Line3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Zip = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "CartLine",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartLine", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CartLine_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CartLine_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartLine_OrderId",
                table: "CartLine",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_CartLine_ProductId",
                table: "CartLine",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_BuyProduct_AspNetUsers_UserId",
                table: "BuyProduct",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BuyProduct_Products_ProductId",
                table: "BuyProduct",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BuyProduct_AspNetUsers_UserId",
                table: "BuyProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_BuyProduct_Products_ProductId",
                table: "BuyProduct");

            migrationBuilder.DropTable(
                name: "CartLine");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BuyProduct",
                table: "BuyProduct");

            migrationBuilder.RenameTable(
                name: "BuyProduct",
                newName: "BuyProducts");

            migrationBuilder.RenameIndex(
                name: "IX_BuyProduct_UserId",
                table: "BuyProducts",
                newName: "IX_BuyProducts_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_BuyProduct_ProductId",
                table: "BuyProducts",
                newName: "IX_BuyProducts_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BuyProducts",
                table: "BuyProducts",
                column: "Id");

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
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
