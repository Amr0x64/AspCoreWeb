using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication3.Migrations
{
    public partial class uhuerhgrugirh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FiasStatmentS",
                table: "FiasStatmentS");

            migrationBuilder.DropColumn(
                name: "FiasStatmentId",
                table: "FiasStatmentS");

            migrationBuilder.DropColumn(
                name: "AddUser",
                table: "FiasStatmentS");

            migrationBuilder.DropColumn(
                name: "NextId",
                table: "FiasStatmentS");

            migrationBuilder.RenameTable(
                name: "FiasStatmentS",
                newName: "FiasStatments");

            migrationBuilder.RenameColumn(
                name: "Level",
                table: "FiasStatments",
                newName: "level");

            migrationBuilder.RenameColumn(
                name: "TypeName",
                table: "FiasStatments",
                newName: "type_name");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "FiasStatments",
                newName: "start_date");

            migrationBuilder.RenameColumn(
                name: "ShortTypeName",
                table: "FiasStatments",
                newName: "short_type_name");

            migrationBuilder.RenameColumn(
                name: "PrevId",
                table: "FiasStatments",
                newName: "add_user");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "FiasStatments",
                newName: "fias_guid");

            migrationBuilder.RenameColumn(
                name: "FiasGuid",
                table: "FiasStatments",
                newName: "fias_statements_id");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "FiasStatments",
                newName: "end_date");

            migrationBuilder.RenameColumn(
                name: "CurrStatus",
                table: "FiasStatments",
                newName: "curr_status");

            migrationBuilder.RenameColumn(
                name: "AddressName",
                table: "FiasStatments",
                newName: "address_name");

            migrationBuilder.RenameColumn(
                name: "ActStatus",
                table: "FiasStatments",
                newName: "act_status");

            migrationBuilder.AddColumn<Guid>(
                name: "next_id",
                table: "FiasStatments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "parent_id",
                table: "FiasStatments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "prev_id",
                table: "FiasStatments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FiasStatments",
                table: "FiasStatments",
                column: "fias_statements_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FiasStatments",
                table: "FiasStatments");

            migrationBuilder.DropColumn(
                name: "next_id",
                table: "FiasStatments");

            migrationBuilder.DropColumn(
                name: "parent_id",
                table: "FiasStatments");

            migrationBuilder.DropColumn(
                name: "prev_id",
                table: "FiasStatments");

            migrationBuilder.RenameTable(
                name: "FiasStatments",
                newName: "FiasStatmentS");

            migrationBuilder.RenameColumn(
                name: "level",
                table: "FiasStatmentS",
                newName: "Level");

            migrationBuilder.RenameColumn(
                name: "type_name",
                table: "FiasStatmentS",
                newName: "TypeName");

            migrationBuilder.RenameColumn(
                name: "start_date",
                table: "FiasStatmentS",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "short_type_name",
                table: "FiasStatmentS",
                newName: "ShortTypeName");

            migrationBuilder.RenameColumn(
                name: "fias_guid",
                table: "FiasStatmentS",
                newName: "ParentId");

            migrationBuilder.RenameColumn(
                name: "end_date",
                table: "FiasStatmentS",
                newName: "EndDate");

            migrationBuilder.RenameColumn(
                name: "curr_status",
                table: "FiasStatmentS",
                newName: "CurrStatus");

            migrationBuilder.RenameColumn(
                name: "address_name",
                table: "FiasStatmentS",
                newName: "AddressName");

            migrationBuilder.RenameColumn(
                name: "add_user",
                table: "FiasStatmentS",
                newName: "PrevId");

            migrationBuilder.RenameColumn(
                name: "act_status",
                table: "FiasStatmentS",
                newName: "ActStatus");

            migrationBuilder.RenameColumn(
                name: "fias_statements_id",
                table: "FiasStatmentS",
                newName: "FiasGuid");

            migrationBuilder.AddColumn<Guid>(
                name: "FiasStatmentId",
                table: "FiasStatmentS",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "AddUser",
                table: "FiasStatmentS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NextId",
                table: "FiasStatmentS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FiasStatmentS",
                table: "FiasStatmentS",
                column: "FiasStatmentId");
        }
    }
}
