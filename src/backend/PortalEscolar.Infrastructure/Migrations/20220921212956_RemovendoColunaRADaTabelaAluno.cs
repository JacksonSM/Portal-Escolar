using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalEscolar.Infrastructure.Migrations
{
    public partial class RemovendoColunaRADaTabelaAluno : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlunoRA",
                table: "Aluno");

            migrationBuilder.UpdateData(
                table: "Diretor",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DataRegistro",
                value: new DateTime(2022, 9, 21, 21, 29, 55, 782, DateTimeKind.Utc).AddTicks(9854));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AlunoRA",
                table: "Aluno",
                type: "bigint",
                nullable: false,
                defaultValueSql: "NULL");

            migrationBuilder.UpdateData(
                table: "Diretor",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DataRegistro",
                value: new DateTime(2022, 9, 21, 20, 26, 32, 639, DateTimeKind.Utc).AddTicks(8942));
        }
    }
}
