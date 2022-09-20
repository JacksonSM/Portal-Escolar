using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalEscolar.Domain.Entities.Diretoria;

namespace PortalEscolar.Infrastructure.EntitiesMap.Diretoria;
public class DiretorMap : IEntityTypeConfiguration<Diretor>
{
    public void Configure(EntityTypeBuilder<Diretor> builder)
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
