using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalEscolar.Infrastructure.Migrations
{
    public partial class SemeandoPapeldoDiretor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Diretor",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DataRegistro",
                value: new DateTime(2022, 9, 16, 19, 50, 52, 96, DateTimeKind.Utc).AddTicks(3177));

            migrationBuilder.UpdateData(
                table: "Papel",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DataRegistro",
                value: new DateTime(2022, 9, 16, 19, 50, 52, 96, DateTimeKind.Utc).AddTicks(2953));

            migrationBuilder.UpdateData(
                table: "Papel",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DataRegistro",
                value: new DateTime(2022, 9, 16, 19, 50, 52, 96, DateTimeKind.Utc).AddTicks(3119));

            migrationBuilder.UpdateData(
                table: "Papel",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DataRegistro",
                value: new DateTime(2022, 9, 16, 19, 50, 52, 96, DateTimeKind.Utc).AddTicks(3141));

            migrationBuilder.InsertData(
                table: "PapelUsuario",
                columns: new[] { "EmailUsuario", "PapelId" },
                values: new object[] { "diretor@portalescolar.com", 1L });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PapelUsuario",
                keyColumns: new[] { "EmailUsuario", "PapelId" },
                keyValues: new object[] { "diretor@portalescolar.com", 1L });

            migrationBuilder.UpdateData(
                table: "Diretor",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DataRegistro",
                value: new DateTime(2022, 9, 16, 16, 33, 44, 397, DateTimeKind.Utc).AddTicks(7967));

            migrationBuilder.UpdateData(
                table: "Papel",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DataRegistro",
                value: new DateTime(2022, 9, 16, 16, 33, 44, 397, DateTimeKind.Utc).AddTicks(7833));

            migrationBuilder.UpdateData(
                table: "Papel",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DataRegistro",
                value: new DateTime(2022, 9, 16, 16, 33, 44, 397, DateTimeKind.Utc).AddTicks(7911));

            migrationBuilder.UpdateData(
                table: "Papel",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DataRegistro",
                value: new DateTime(2022, 9, 16, 16, 33, 44, 397, DateTimeKind.Utc).AddTicks(7931));
        }
    }
}
