using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeskJr.Data.Migrations
{
    public partial class v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "EmployeeTitles",
                columns: new[] { "ID", "TitleName" },
                values: new object[] { new Guid("2bee94c8-56fe-4b90-8e0b-4789b6dca2df"), "Default Title" });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "ID", "ManagerId", "Name" },
                values: new object[] { new Guid("98ec940f-0387-4bf7-b847-78d23184729a"), null, "Default Team" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "ID", "DayOfBirth", "Email", "EmployeeRole", "EmployeeTitleId", "Gender", "Name", "Password", "TeamId" },
                values: new object[] { new Guid("1a3627b3-208e-49b1-8b8c-334d805a3a65"), new DateTime(2001, 8, 1, 17, 16, 39, 802, DateTimeKind.Local).AddTicks(4770), "admin@deskjr.com", 0, new Guid("2bee94c8-56fe-4b90-8e0b-4789b6dca2df"), 0, "Admin", "202CB962AC59075B964B07152D234B70", new Guid("98ec940f-0387-4bf7-b847-78d23184729a") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "ID",
                keyValue: new Guid("1a3627b3-208e-49b1-8b8c-334d805a3a65"));

            migrationBuilder.DeleteData(
                table: "EmployeeTitles",
                keyColumn: "ID",
                keyValue: new Guid("2bee94c8-56fe-4b90-8e0b-4789b6dca2df"));

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "ID",
                keyValue: new Guid("98ec940f-0387-4bf7-b847-78d23184729a"));
        }
    }
}
