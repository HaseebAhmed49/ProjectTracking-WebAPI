using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectTracking_WebAPI.Migrations
{
    public partial class ChangedInModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTask_Employee_EmployeeEmoloyeeID",
                table: "ProjectTask");

            migrationBuilder.RenameColumn(
                name: "EmployeeEmoloyeeID",
                table: "ProjectTask",
                newName: "EmployeeID");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectTask_EmployeeEmoloyeeID",
                table: "ProjectTask",
                newName: "IX_ProjectTask_EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTask_Employee_EmployeeID",
                table: "ProjectTask",
                column: "EmployeeID",
                principalTable: "Employee",
                principalColumn: "EmoloyeeID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTask_Employee_EmployeeID",
                table: "ProjectTask");

            migrationBuilder.RenameColumn(
                name: "EmployeeID",
                table: "ProjectTask",
                newName: "EmployeeEmoloyeeID");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectTask_EmployeeID",
                table: "ProjectTask",
                newName: "IX_ProjectTask_EmployeeEmoloyeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTask_Employee_EmployeeEmoloyeeID",
                table: "ProjectTask",
                column: "EmployeeEmoloyeeID",
                principalTable: "Employee",
                principalColumn: "EmoloyeeID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
