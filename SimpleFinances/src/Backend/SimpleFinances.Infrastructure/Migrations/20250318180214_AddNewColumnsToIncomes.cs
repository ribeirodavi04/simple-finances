using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleFinances.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNewColumnsToIncomes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
    
            migrationBuilder.AddColumn<DateTime>(
                name: "DateReceived",
                table: "Incomes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsRecurring",
                table: "Incomes",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateReceived",
                table: "Incomes");

            migrationBuilder.DropColumn(
                name: "IsRecurring",
                table: "Incomes");         
        }
    }
}
