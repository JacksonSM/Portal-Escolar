using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalEscolar.Domain.Entities.Papel;

namespace PortalEscolar.Infrastructure.EntitiesMap.Papeis;
public class PapelUsuarioMap : IEntityTypeConfiguration<PapelUsuario>
{
    public void Configure(EntityTypeBuilder<PapelUsuario> builder)
    {
        builder.HasKey(x => new { x.PapelId, x.EmailUsuario });

        builder.HasOne(r => r.Papel)                            
            .WithMany()
            .HasForeignKey(c => c.PapelId)
            .IsRequired();
    }
}
