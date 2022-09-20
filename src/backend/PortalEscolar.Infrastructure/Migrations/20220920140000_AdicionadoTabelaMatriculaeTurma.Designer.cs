﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PortalEscolar.Infrastructure.Context;

#nullable disable

namespace PortalEscolar.Infrastructure.Migrations
{
    [DbContext(typeof(PortalEscolarDbContext))]
    [Migration("20220920140000_AdicionadoTabelaMatriculaeTurma")]
    partial class AdicionadoTabelaMatriculaeTurma
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PortalEscolar.Domain.Entities.Diretoria.Diretor", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("date");

                    b.Property<DateTime>("DataRegistro")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("NomeCompleto")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Papel")
                        .HasColumnType("int");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Diretor");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            DataNascimento = new DateTime(2002, 9, 20, 0, 0, 0, 0, DateTimeKind.Local),
                            DataRegistro = new DateTime(2022, 9, 20, 13, 59, 59, 653, DateTimeKind.Utc).AddTicks(7469),
                            Email = "diretor@portalescolar.com",
                            NomeCompleto = "Diretor",
                            Papel = 1,
                            Senha = "Diretor321"
                        });
                });

            modelBuilder.Entity("PortalEscolar.Domain.Entities.Diretoria.Matricula.Matricula", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long>("AlunoId")
                        .HasColumnType("bigint");

                    b.Property<string>("CidadeNascimentoAluno")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<DateTime>("DataInicio")
                        .HasColumnType("date");

                    b.Property<DateTime>("DataNascimentoAluno")
                        .HasColumnType("date");

                    b.Property<DateTime>("DataRegistro")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataTerminio")
                        .HasColumnType("date");

                    b.Property<string>("NomeCompletoAluno")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<long>("TurmaId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AlunoId");

                    b.HasIndex("TurmaId");

                    b.ToTable("Matricula");
                });

            modelBuilder.Entity("PortalEscolar.Domain.Entities.SalaAula.Aluno", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long>("AlunoRA")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("date");

                    b.Property<DateTime>("DataRegistro")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("NomeCompleto")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Papel")
                        .HasColumnType("int");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("TurmaId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("TurmaId");

                    b.ToTable("Aluno");
                });

            modelBuilder.Entity("PortalEscolar.Domain.Entities.SalaAula.Professora", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("date");

                    b.Property<DateTime>("DataRegistro")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("NomeCompleto")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Papel")
                        .HasColumnType("int");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Professora");
                });

            modelBuilder.Entity("PortalEscolar.Domain.Entities.SalaAula.Turma", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DataRegistro")
                        .HasColumnType("datetime2");

                    b.Property<string>("NomeTurma")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("NomeTurma");

                    b.Property<long>("ProfessoraId")
                        .HasColumnType("bigint");

                    b.Property<string>("Sala")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Sala");

                    b.Property<int>("Serie")
                        .HasColumnType("int");

                    b.Property<int>("Turno")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProfessoraId");

                    b.ToTable("Turma");
                });

            modelBuilder.Entity("PortalEscolar.Domain.Entities.Diretoria.Matricula.Matricula", b =>
                {
                    b.HasOne("PortalEscolar.Domain.Entities.SalaAula.Aluno", "Aluno")
                        .WithMany("Matricula")
                        .HasForeignKey("AlunoId")
                        .IsRequired();

                    b.HasOne("PortalEscolar.Domain.Entities.SalaAula.Turma", "Turma")
                        .WithMany("Matricula")
                        .HasForeignKey("TurmaId")
                        .IsRequired();

                    b.OwnsOne("PortalEscolar.Domain.Entities.Diretoria.Matricula.Responsavel", "Responsavel", b1 =>
                        {
                            b1.Property<long>("MatriculaId")
                                .HasColumnType("bigint");

                            b1.Property<string>("CPF")
                                .IsRequired()
                                .HasColumnType("varchar(11)")
                                .HasColumnName("ResponsavelCPF");

                            b1.Property<string>("Cidade")
                                .IsRequired()
                                .HasColumnType("varchar(100)")
                                .HasColumnName("ResponsavelCidade");

                            b1.Property<DateTime>("DataNascimento")
                                .HasColumnType("datetime2")
                                .HasColumnName("ResponsavelDatanascimento");

                            b1.Property<string>("NomeCompleto")
                                .IsRequired()
                                .HasColumnType("varchar(200)")
                                .HasColumnName("ResponsavelNomecompleto");

                            b1.Property<string>("Telefone")
                                .IsRequired()
                                .HasColumnType("varchar(20)")
                                .HasColumnName("ResponsavelTelefone");

                            b1.HasKey("MatriculaId");

                            b1.ToTable("Matricula");

                            b1.WithOwner()
                                .HasForeignKey("MatriculaId");
                        });

                    b.Navigation("Aluno");

                    b.Navigation("Responsavel");

                    b.Navigation("Turma");
                });

            modelBuilder.Entity("PortalEscolar.Domain.Entities.SalaAula.Aluno", b =>
                {
                    b.HasOne("PortalEscolar.Domain.Entities.SalaAula.Turma", null)
                        .WithMany("Alunos")
                        .HasForeignKey("TurmaId");
                });

            modelBuilder.Entity("PortalEscolar.Domain.Entities.SalaAula.Turma", b =>
                {
                    b.HasOne("PortalEscolar.Domain.Entities.SalaAula.Professora", "Professora")
                        .WithMany("Turma")
                        .HasForeignKey("ProfessoraId")
                        .IsRequired();

                    b.Navigation("Professora");
                });

            modelBuilder.Entity("PortalEscolar.Domain.Entities.SalaAula.Aluno", b =>
                {
                    b.Navigation("Matricula");
                });

            modelBuilder.Entity("PortalEscolar.Domain.Entities.SalaAula.Professora", b =>
                {
                    b.Navigation("Turma");
                });

            modelBuilder.Entity("PortalEscolar.Domain.Entities.SalaAula.Turma", b =>
                {
                    b.Navigation("Alunos");

                    b.Navigation("Matricula");
                });
#pragma warning restore 612, 618
        }
    }
}