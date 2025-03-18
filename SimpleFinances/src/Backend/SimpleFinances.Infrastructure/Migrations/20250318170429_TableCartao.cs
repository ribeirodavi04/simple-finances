using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SimpleFinances.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TableCartao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cartao",
                columns: table => new
                {
                    CartaoId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CartaGuid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    CartaoNome = table.Column<string>(type: "text", nullable: false),
                    CartaoTipo = table.Column<string>(type: "text", nullable: false),
                    CartaoBanco = table.Column<string>(type: "text", nullable: false),
                    CartaoLimite = table.Column<decimal>(type: "numeric", nullable: false),
                    CartaoDataVencimento = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CartaoDataFechamento = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CartaoUsuarioId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cartoes", x => x.CartaoId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cartao");
        }
    }
}
