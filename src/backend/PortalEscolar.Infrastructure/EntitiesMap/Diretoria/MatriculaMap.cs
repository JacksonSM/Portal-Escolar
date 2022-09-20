using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalEscolar.Domain.Entities.Diretoria.Matricula;

namespace PortalEscolar.Infrastructure.EntitiesMap.Diretoria;
public class MatriculaMap : IEntityTypeConfiguration<Matricula>
{
    public void Configure(EntityTypeBuilder<Matricula> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.NomeCompletoAluno)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.CidadeNascimentoAluno)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(c => c.DataNascimentoAluno)
            .HasColumnType("date");

        builder.Property(c => c.DataInicio)
            .HasColumnType("date");

        builder.Property(c => c.DataTerminio)
            .HasColumnType("date");

        builder.OwnsOne(c => c.Responsavel, responsavel =>
        {
            responsavel.Property(c => c.NomeCompleto)
                .IsRequired()
                .HasColumnName("ResponsavelNomecompleto")
                .HasColumnType("varchar(200)");

            responsavel.Property(c => c.CPF)
                .IsRequired()
                .HasColumnName("ResponsavelCPF")
                .HasColumnType("varchar(11)");

            responsavel.Property(c => c.Cidade)
                .IsRequired()
                .HasColumnName("ResponsavelCidade")
                .HasColumnType("varchar(100)");

            responsavel.Property(c => c.DataNascimento)
                .IsRequired()
                .HasColumnName("ResponsavelDatanascimento");

            responsavel.Property(c => c.Telefone)
                .IsRequired()
                .HasColumnName("ResponsavelTelefone")
                .HasColumnType("varchar(20)");
        });



    }
}
