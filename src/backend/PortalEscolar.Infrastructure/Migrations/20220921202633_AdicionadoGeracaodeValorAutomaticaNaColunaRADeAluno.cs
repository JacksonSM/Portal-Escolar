using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalEscolar.Infrastructure.Migrations
{
    public partial class AdicionadoGeracaodeValorAutomaticaNaColunaRADeAluno : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "AlunoRA",
                table: "Aluno",
                type: "bigint",
                nullable: false,
                defaultValueSql: "NULL",
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.UpdateData(
                table: "Diretor",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DataNascimento", "DataRegistro" },
                values: new object[] { new DateTime(2002, 9, 21, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 9, 21, 20, 26, 32, 639, DateTimeKind.Utc).AddTicks(8942) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "AlunoRA",
                table: "Aluno",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldDefaultValueSql: "NULL");

            migrationBuilder.UpdateData(
                table: "Diretor",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DataNascimento", "DataRegistro" },
                values: new object[] { new DateTime(2002, 9, 20, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 9, 20, 22, 40, 48, 764, DateTimeKind.Utc).AddTicks(2134) });
        }
    }
}
