using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalEscolar.Infrastructure.Migrations
{
    public partial class ResertadoAsMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Diretor",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCompleto = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "date", nullable: false),
                    DataRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diretor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Papel",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomeNormalizado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Papel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Professora",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCompleto = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "date", nullable: false),
                    DataRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professora", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PapelUsuario",
                columns: table => new
                {
                    EmailUsuario = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PapelId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PapelUsuario", x => new { x.PapelId, x.EmailUsuario });
                    table.ForeignKey(
                        name: "FK_PapelUsuario_Papel_PapelId",
                        column: x => x.PapelId,
                        principalTable: "Papel",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Diretor",
                columns: new[] { "Id", "DataNascimento", "DataRegistro", "Email", "NomeCompleto", "Senha" },
                values: new object[] { 1L, new DateTime(2002, 9, 16, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 9, 16, 16, 33, 44, 397, DateTimeKind.Utc).AddTicks(7967), "diretor@portalescolar.com", "Diretor", "Diretor321" });

            migrationBuilder.InsertData(
                table: "Papel",
                columns: new[] { "Id", "DataRegistro", "Nome", "NomeNormalizado" },
                values: new object[,]
                {
                    { 1L, new DateTime(2022, 9, 16, 16, 33, 44, 397, DateTimeKind.Utc).AddTicks(7833), "Diretor", "DIRETOR" },
                    { 2L, new DateTime(2022, 9, 16, 16, 33, 44, 397, DateTimeKind.Utc).AddTicks(7911), "Professora", "PROFESSORA" },
                    { 3L, new DateTime(2022, 9, 16, 16, 33, 44, 397, DateTimeKind.Utc).AddTicks(7931), "Aluno", "ALUNO" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Diretor");

            migrationBuilder.DropTable(
                name: "PapelUsuario");

            migrationBuilder.DropTable(
                name: "Professora");

            migrationBuilder.DropTable(
                name: "Papel");
        }
    }
}
