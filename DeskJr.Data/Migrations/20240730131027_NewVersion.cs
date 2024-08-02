using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeskJr.Data.Migrations
{
    public partial class NewVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "LeaveTypes");

            migrationBuilder.DropColumn(
                name: "DefaultDays",
                table: "LeaveTypes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "LeaveTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DefaultDays",
                table: "LeaveTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
