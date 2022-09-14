using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalEscolar.Domain.Entities.SalaAula;

namespace PortalEscolar.Infrastructure.EntitiesMap.SalaAula;
public class ProfessoraMap : IEntityTypeConfiguration<Professora>
{
    public void Configure(EntityTypeBuilder<Professora> builder)
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

        builder.Property<DateTime>(c => c.DataNascimento)
               .HasColumnType("date");
    }
}

