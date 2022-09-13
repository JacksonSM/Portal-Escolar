using Microsoft.EntityFrameworkCore;
using PortalEscolar.Domain.Entities.Diretoria;


namespace PortalEscolar.Infrastructure.Context;
public class PortalEscolarDbContext : DbContext
{
    public PortalEscolarDbContext(DbContextOptions<PortalEscolarDbContext> options) : base(options) { }

    public DbSet<Diretor> Diretor { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
        
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(PortalEscolarDbContext).Assembly);
    }
}
