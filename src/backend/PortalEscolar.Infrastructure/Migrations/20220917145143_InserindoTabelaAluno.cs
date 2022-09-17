using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalEscolar.Infrastructure.Migrations
{
    public partial class InserindoTabelaAluno : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aluno",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCompleto = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "date", nullable: false),
                    DataRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Papel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aluno", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Diretor",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DataNascimento", "DataRegistro" },
                values: new object[] { new DateTime(2002, 9, 17, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 9, 17, 14, 51, 42, 667, DateTimeKind.Utc).AddTicks(6650) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aluno");

            migrationBuilder.UpdateData(
                table: "Diretor",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DataNascimento", "DataRegistro" },
                values: new object[] { new DateTime(2002, 9, 16, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 9, 16, 23, 6, 23, 595, DateTimeKind.Utc).AddTicks(1537) });
        }
    }
}
