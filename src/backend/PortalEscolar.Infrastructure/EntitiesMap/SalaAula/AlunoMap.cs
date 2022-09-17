using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalEscolar.Domain.Entities.SalaAula;

namespace PortalEscolar.Infrastructure.EntitiesMap.SalaAula;
public class AlunoMap : IEntityTypeConfiguration<Aluno>
{
    public void Configure(EntityTypeBuilder<Aluno> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(c => c.NomeCompleto)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.Email)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.Senha)
            .IsRequired();

        builder.Property(c => c.DataNascimento)
               .HasColumnType("date");
    }
}

