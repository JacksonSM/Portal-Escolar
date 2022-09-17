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
    [Migration("20220916163344_ResertadoAsMigrations")]
    partial class ResertadoAsMigrations
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

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Diretor");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            DataNascimento = new DateTime(2002, 9, 16, 0, 0, 0, 0, DateTimeKind.Local),
                            DataRegistro = new DateTime(2022, 9, 16, 16, 33, 44, 397, DateTimeKind.Utc).AddTicks(7967),
                            Email = "diretor@portalescolar.com",
                            NomeCompleto = "Diretor",
                            Senha = "Diretor321"
                        });
                });

            modelBuilder.Entity("PortalEscolar.Domain.Entities.Papel.Papel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("DataRegistro")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomeNormalizado")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Papel");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            DataRegistro = new DateTime(2022, 9, 16, 16, 33, 44, 397, DateTimeKind.Utc).AddTicks(7833),
                            Nome = "Diretor",
                            NomeNormalizado = "DIRETOR"
                        },
                        new
                        {
                            Id = 2L,
                            DataRegistro = new DateTime(2022, 9, 16, 16, 33, 44, 397, DateTimeKind.Utc).AddTicks(7911),
                            Nome = "Professora",
                            NomeNormalizado = "PROFESSORA"
                        },
                        new
                        {
                            Id = 3L,
                            DataRegistro = new DateTime(2022, 9, 16, 16, 33, 44, 397, DateTimeKind.Utc).AddTicks(7931),
                            Nome = "Aluno",
                            NomeNormalizado = "ALUNO"
                        });
                });

            modelBuilder.Entity("PortalEscolar.Domain.Entities.Papel.PapelUsuario", b =>
                {
                    b.Property<long>("PapelId")
                        .HasColumnType("bigint");

                    b.Property<string>("EmailUsuario")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("PapelId", "EmailUsuario");

                    b.ToTable("PapelUsuario");
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

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Professora");
                });

            modelBuilder.Entity("PortalEscolar.Domain.Entities.Papel.PapelUsuario", b =>
                {
                    b.HasOne("PortalEscolar.Domain.Entities.Papel.Papel", "Papel")
                        .WithMany()
                        .HasForeignKey("PapelId")
                        .IsRequired();

                    b.Navigation("Papel");
                });
#pragma warning restore 612, 618
        }
    }
}