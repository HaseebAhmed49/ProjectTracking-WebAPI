using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectTracking_WebAPI.Migrations
{
    public partial class EmployeeTableIdColumnChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmoloyeeID",
                table: "Employee",
                newName: "EmployeeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmployeeID",
                table: "Employee",
                newName: "EmoloyeeID");
        }
    }
}
