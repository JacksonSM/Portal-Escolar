using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalEscolar.Infrastructure.Migrations
{
    public partial class AdicionadoTabelaMatriculaETurma : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AlunoRA",
                table: "Aluno",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TurmaId",
                table: "Aluno",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Turma",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfessoraId = table.Column<long>(type: "bigint", nullable: false),
                    Sala = table.Column<string>(type: "varchar(50)", nullable: false),
                    NomeTurma = table.Column<string>(type: "varchar(50)", nullable: false),
                    Serie = table.Column<int>(type: "int", nullable: false),
                    Turno = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turma", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Turma_Professora_ProfessoraId",
                        column: x => x.ProfessoraId,
                        principalTable: "Professora",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "matricula",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCompletoAluno = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DataNascimentoAluno = table.Column<DateTime>(type: "date", nullable: false),
                    CidadeNascimentoAluno = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DataInicio = table.Column<DateTime>(type: "date", nullable: false),
                    DataTerminio = table.Column<DateTime>(type: "date", nullable: false),
                    ResponsavelNomecompleto = table.Column<string>(type: "varchar(200)", nullable: true),
                    ResponsavelDatanascimento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ResponsavelTelefone = table.Column<string>(type: "varchar(20)", nullable: true),
                    ResponsavelCPF = table.Column<string>(type: "varchar(11)", nullable: true),
                    ResponsavelCidade = table.Column<string>(type: "varchar(100)", nullable: true),
                    AlunoId = table.Column<long>(type: "bigint", nullable: false),
                    TurmaId = table.Column<long>(type: "bigint", nullable: false),
                    DataRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_matricula", x => x.Id);
                    table.ForeignKey(
                        name: "FK_matricula_Aluno_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Aluno",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_matricula_Turma_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turma",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "Diretor",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DataNascimento", "DataRegistro" },
                values: new object[] { new DateTime(2002, 9, 20, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 9, 20, 22, 40, 48, 764, DateTimeKind.Utc).AddTicks(2134) });

            migrationBuilder.CreateIndex(
                name: "IX_Aluno_TurmaId",
                table: "Aluno",
                column: "TurmaId");

            migrationBuilder.CreateIndex(
                name: "IX_matricula_AlunoId",
                table: "matricula",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_matricula_TurmaId",
                table: "matricula",
                column: "TurmaId");

            migrationBuilder.CreateIndex(
                name: "IX_Turma_ProfessoraId",
                table: "Turma",
                column: "ProfessoraId");

            migrationBuilder.AddForeignKey(
                name: "FK_Aluno_Turma_TurmaId",
                table: "Aluno",
                column: "TurmaId",
                principalTable: "Turma",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aluno_Turma_TurmaId",
                table: "Aluno");

            migrationBuilder.DropTable(
                name: "matricula");

            migrationBuilder.DropTable(
                name: "Turma");

            migrationBuilder.DropIndex(
                name: "IX_Aluno_TurmaId",
                table: "Aluno");

            migrationBuilder.DropColumn(
                name: "AlunoRA",
                table: "Aluno");

            migrationBuilder.DropColumn(
                name: "TurmaId",
                table: "Aluno");

            migrationBuilder.UpdateData(
                table: "Diretor",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "DataNascimento", "DataRegistro" },
                values: new object[] { new DateTime(2002, 9, 17, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 9, 17, 14, 51, 42, 667, DateTimeKind.Utc).AddTicks(6650) });
        }
    }
}
