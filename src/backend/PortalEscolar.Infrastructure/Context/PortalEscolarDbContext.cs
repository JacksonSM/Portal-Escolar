using Microsoft.EntityFrameworkCore;
using PortalEscolar.Domain.Entities.Diretoria;
using PortalEscolar.Domain.Entities.Diretoria.Matricula;
using PortalEscolar.Domain.Entities.SalaAula;

namespace PortalEscolar.Infrastructure.Context;
public class PortalEscolarDbContext : DbContext
{
    public PortalEscolarDbContext(DbContextOptions<PortalEscolarDbContext> options) : base(options) { }

    public DbSet<Diretor> Diretor { get; set; }
    public DbSet<Professora> Professora { get; set; }
    public DbSet<Aluno> Aluno { get; set; }
    public DbSet<Turma> Turma { get; set; }
    public DbSet<Matricula> Matricula { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

        builder.SemearDiretor();

        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(PortalEscolarDbContext).Assembly);
    }

    
}
