using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeskJr.Data.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Teams_TeamId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Leaves_Employees_RequestingEmployeeId",
                table: "Leaves");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Employees_ManagerId",
                table: "Teams");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Teams_TeamId",
                table: "Employees",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Leaves_Employees_RequestingEmployeeId",
                table: "Leaves",
                column: "RequestingEmployeeId",
                principalTable: "Employees",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Employees_ManagerId",
                table: "Teams",
                column: "ManagerId",
                principalTable: "Employees",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Teams_TeamId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Leaves_Employees_RequestingEmployeeId",
                table: "Leaves");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Employees_ManagerId",
                table: "Teams");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Teams_TeamId",
                table: "Employees",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Leaves_Employees_RequestingEmployeeId",
                table: "Leaves",
                column: "RequestingEmployeeId",
                principalTable: "Employees",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Employees_ManagerId",
                table: "Teams",
                column: "ManagerId",
                principalTable: "Employees",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
