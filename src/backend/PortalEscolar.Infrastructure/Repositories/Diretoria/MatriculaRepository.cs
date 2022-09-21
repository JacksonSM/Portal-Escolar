using PortalEscolar.Domain.Entities.Diretoria.Matricula;
using PortalEscolar.Domain.Interfaces.Repositories.Diretoria.Matricula;
using PortalEscolar.Infrastructure.Context;

namespace PortalEscolar.Infrastructure.Repositories.Diretoria;
public class MatriculaRepository : IMatriculaWriteOnlyRepository
{
    private readonly PortalEscolarDbContext _context;

    public MatriculaRepository(PortalEscolarDbContext context)
    {
        _context = context;
    }

    public async Task AdicionarAsync(Matricula matricula) =>
        await _context.matricula.AddAsync(matricula);   
}
