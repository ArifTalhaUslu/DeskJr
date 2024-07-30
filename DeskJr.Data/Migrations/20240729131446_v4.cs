using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeskJr.Data.Migrations
{
    public partial class v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeTitles_TitleId",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "TitleId",
                table: "Employees",
                newName: "EmployeeTitleId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_TitleId",
                table: "Employees",
                newName: "IX_Employees_EmployeeTitleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeeTitles_EmployeeTitleId",
                table: "Employees",
                column: "EmployeeTitleId",
                principalTable: "EmployeeTitles",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeTitles_EmployeeTitleId",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "EmployeeTitleId",
                table: "Employees",
                newName: "TitleId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_EmployeeTitleId",
                table: "Employees",
                newName: "IX_Employees_TitleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeeTitles_TitleId",
                table: "Employees",
                column: "TitleId",
                principalTable: "EmployeeTitles",
                principalColumn: "ID");
        }
    }
}
