using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalEscolar.Infrastructure.Migrations
{
    public partial class RemovendoTabelasPapelePapelUsuarioeAdicionadoCampoPapel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PapelUsuario");

            migrationBuilder.DropTable(
                name: "Papel");

            migrationBuilder.AddColumn<int>(
                name: "Papel",
                table: "Professora",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Papel",
                table: "Diretor",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Diretor",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DataRegistro", "Papel" },
                values: new object[] { new DateTime(2022, 9, 16, 23, 6, 23, 595, DateTimeKind.Utc).AddTicks(1537), 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Papel",
                table: "Professora");

            migrationBuilder.DropColumn(
                name: "Papel",
                table: "Diretor");

            migrationBuilder.CreateTable(
                name: "Papel",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomeNormalizado = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Papel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PapelUsuario",
                columns: table => new
                {
                    PapelId = table.Column<long>(type: "bigint", nullable: false),
                    EmailUsuario = table.Column<string>(type: "nvarchar(450)", nullable: false)
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

            migrationBuilder.UpdateData(
                table: "Diretor",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DataRegistro",
                value: new DateTime(2022, 9, 16, 19, 50, 52, 96, DateTimeKind.Utc).AddTicks(3177));

            migrationBuilder.InsertData(
                table: "Papel",
                columns: new[] { "Id", "DataRegistro", "Nome", "NomeNormalizado" },
                values: new object[,]
                {
                    { 1L, new DateTime(2022, 9, 16, 19, 50, 52, 96, DateTimeKind.Utc).AddTicks(2953), "Diretor", "DIRETOR" },
                    { 2L, new DateTime(2022, 9, 16, 19, 50, 52, 96, DateTimeKind.Utc).AddTicks(3119), "Professora", "PROFESSORA" },
                    { 3L, new DateTime(2022, 9, 16, 19, 50, 52, 96, DateTimeKind.Utc).AddTicks(3141), "Aluno", "ALUNO" }
                });

            migrationBuilder.InsertData(
                table: "PapelUsuario",
                columns: new[] { "EmailUsuario", "PapelId" },
                values: new object[] { "diretor@portalescolar.com", 1L });
        }
    }
}
