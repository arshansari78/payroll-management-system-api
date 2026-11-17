using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayrollManagementSystem.Api.Migrations
{
    /// <inheritdoc />
    public partial class tyui : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalaryStructures_Employees_EmployeeCode",
                table: "SalaryStructures");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Employees_EmployeeCode",
                table: "Employees",
                column: "EmployeeCode");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "administrator",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "53ecf20f-e7c6-4726-b685-39971fc126ed", "AQAAAAIAAYagAAAAEAUtmwPjmi5HA6zNtfH2rnCHFIFGGsICe51CrSSX1HZBY4Ez0MgNoDxoPJjtzAid3Q==", "9eb7a20d-405d-4156-a007-b947797fa496" });

            migrationBuilder.AddForeignKey(
                name: "FK_SalaryStructures_Employees_EmployeeCode",
                table: "SalaryStructures",
                column: "EmployeeCode",
                principalTable: "Employees",
                principalColumn: "EmployeeCode",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalaryStructures_Employees_EmployeeCode",
                table: "SalaryStructures");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Employees_EmployeeCode",
                table: "Employees");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "administrator",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d16d4e9b-d0bd-4cb6-a95b-36b7907cd5fd", "AQAAAAIAAYagAAAAEOxjrsmqvudkrfpEX4oYByqB2KM9KHhK5BlQ9l3nu+CMqm9AF3OkfRNFy3/ruCCEmg==", "f44627c6-4b0c-4383-a58d-9cc545d22fba" });

            migrationBuilder.AddForeignKey(
                name: "FK_SalaryStructures_Employees_EmployeeCode",
                table: "SalaryStructures",
                column: "EmployeeCode",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
