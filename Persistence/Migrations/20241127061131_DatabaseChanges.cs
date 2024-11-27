using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DatabaseChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Applicants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LoanApplicationss",
                table: "LoanApplicationss");

            migrationBuilder.RenameTable(
                name: "LoanApplicationss",
                newName: "LoanApplications");

            migrationBuilder.RenameColumn(
                name: "CurrentIncome",
                table: "LoanApplications",
                newName: "MonthlyDebt");

            migrationBuilder.RenameColumn(
                name: "CurrentExpenses",
                table: "LoanApplications",
                newName: "GrossIncome");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LoanApplications",
                table: "LoanApplications",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "LoanApplicants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanApplicants", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoanApplicants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LoanApplications",
                table: "LoanApplications");

            migrationBuilder.RenameTable(
                name: "LoanApplications",
                newName: "LoanApplicationss");

            migrationBuilder.RenameColumn(
                name: "MonthlyDebt",
                table: "LoanApplicationss",
                newName: "CurrentIncome");

            migrationBuilder.RenameColumn(
                name: "GrossIncome",
                table: "LoanApplicationss",
                newName: "CurrentExpenses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LoanApplicationss",
                table: "LoanApplicationss",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Applicants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IDNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applicants", x => x.Id);
                });
        }
    }
}
