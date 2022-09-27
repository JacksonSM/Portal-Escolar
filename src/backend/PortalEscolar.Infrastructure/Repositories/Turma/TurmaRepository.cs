using Microsoft.EntityFrameworkCore;
using PortalEscolar.Domain.Interfaces.Repositories.Turma;
using PortalEscolar.Infrastructure.Context;

namespace PortalEscolar.Infrastructure.Repositories.Turma;
public class TurmaRepository : ITurmaWriteOnlyRepository, ITurmaReadOnlyRepository
{
    private readonly PortalEscolarDbContext _context;

    public TurmaRepository(PortalEscolarDbContext context)
    {
        _context = context;
    }
    public async Task AdicionarAsync(Domain.Entities.SalaAula.Turma turma) =>
        await _context.Turma.AddAsync(turma);

    public async Task<bool> ExisteTurmaAtivaPorIdAsync(long id) =>
        await _context.Turma.AnyAsync(c => c.Id == id && c.Ativo == true);
}
