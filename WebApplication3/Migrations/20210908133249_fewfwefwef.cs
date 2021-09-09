using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication3.Migrations
{
    public partial class fewfwefwef : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StrucNumber",
                table: "StreetNumbers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StreetNumbers_FiasStatments_FiasStatmentfias_statements_id",
                table: "StreetNumbers");

            migrationBuilder.DropIndex(
                name: "IX_StreetNumbers_FiasStatmentfias_statements_id",
                table: "StreetNumbers");

            migrationBuilder.DropColumn(
                name: "FiasStatmentfias_statements_id",
                table: "StreetNumbers");

            migrationBuilder.AlterColumn<int>(
                name: "StrucNumber",
                table: "StreetNumbers",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FlatType",
                table: "StreetNumbers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StreetNumbers_FiasGuid",
                table: "StreetNumbers",
                column: "FiasGuid");
        }
    }
}
