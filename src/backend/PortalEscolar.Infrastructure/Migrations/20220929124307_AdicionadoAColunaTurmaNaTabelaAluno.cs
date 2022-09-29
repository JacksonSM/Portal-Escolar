using Microsoft.EntityFrameworkCore.Migrations;

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalEscolar.Infrastructure.Migrations
{
    public partial class AdicionadoAColunaTurmaNaTabelaAluno : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "TurmaId",
                table: "Aluno",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: false);

            migrationBuilder.UpdateData(
                table: "Diretor",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DataNascimento", "DataRegistro" },
                values: new object[] { new DateTime(2002, 9, 29, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 9, 29, 12, 43, 6, 462, DateTimeKind.Utc).AddTicks(9471) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "TurmaId",
                table: "Aluno",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.UpdateData(
                table: "Diretor",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DataNascimento", "DataRegistro" },
                values: new object[] { new DateTime(2002, 9, 21, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 9, 21, 21, 29, 55, 782, DateTimeKind.Utc).AddTicks(9854) });
        }
    }
}
