using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication3.Migrations
{
    public partial class iofhewhfuowefoef : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StreetNumbers",
                columns: table => new
                {
                    StreetNumberID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FiasGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HouseNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StrucNumber = table.Column<int>(type: "int", nullable: true),
                    Counter = table.Column<int>(type: "int", nullable: true),
                    FlatNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FlatType = table.Column<int>(type: "int", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrevId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NextId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LiveStatus = table.Column<int>(type: "int", nullable: false),
                    AddUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Startdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StreetNumbers", x => x.StreetNumberID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StreetNumbers_FiasGuid",
                table: "StreetNumbers",
                column: "FiasGuid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StreetNumbers");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_FiasStatments_fias_guid",
                table: "FiasStatments");
        }
    }
}
