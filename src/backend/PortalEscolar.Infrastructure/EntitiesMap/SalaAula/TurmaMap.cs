using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalEscolar.Domain.Entities.SalaAula;

namespace PortalEscolar.Infrastructure.EntitiesMap.SalaAula;
public class TurmaMap : IEntityTypeConfiguration<Turma>
{
    public void Configure(EntityTypeBuilder<Turma> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Sala)
                .IsRequired()
                .HasColumnName("Sala")
                .HasColumnType("varchar(50)");
        
        builder.Property(c => c.NomeTurma)
                .IsRequired()
                .HasColumnName("NomeTurma")
                .HasColumnType("varchar(50)");
    }
}
