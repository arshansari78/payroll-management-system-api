using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayrollManagementSystem.Api.Migrations
{
    /// <inheritdoc />
    public partial class njnuk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payrolls_Employees_EmployeeCode",
                table: "Payrolls");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "administrator",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "510cb4ee-0b82-4a37-b884-ec0be1783686", "AQAAAAIAAYagAAAAEAjBMWhfOfW9JtIOr9ES3wZK3ngySE1WnXMTSxfgV1hIYrWzYKBMxlY+xm0dHcZ81g==", "fdab1fab-b71e-4ce0-99db-a5c053c1256b" });

            migrationBuilder.AddForeignKey(
                name: "FK_Payrolls_Employees_EmployeeCode",
                table: "Payrolls",
                column: "EmployeeCode",
                principalTable: "Employees",
                principalColumn: "EmployeeCode",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payrolls_Employees_EmployeeCode",
                table: "Payrolls");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "administrator",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "53ecf20f-e7c6-4726-b685-39971fc126ed", "AQAAAAIAAYagAAAAEAUtmwPjmi5HA6zNtfH2rnCHFIFGGsICe51CrSSX1HZBY4Ez0MgNoDxoPJjtzAid3Q==", "9eb7a20d-405d-4156-a007-b947797fa496" });

            migrationBuilder.AddForeignKey(
                name: "FK_Payrolls_Employees_EmployeeCode",
                table: "Payrolls",
                column: "EmployeeCode",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
