using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleFinances.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCardRelationshipFromExpense : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Incomes_Cards_CardId",
                table: "Expenses");

            migrationBuilder.AlterColumn<int>(
                name: "CardId",
                table: "Expenses",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }

    }
}
