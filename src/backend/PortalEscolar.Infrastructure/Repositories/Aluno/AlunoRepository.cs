using Microsoft.EntityFrameworkCore;
using PortalEscolar.Domain.Interfaces.Repositories.SalaAula.Aluno;
using PortalEscolar.Infrastructure.Context;

namespace PortalEscolar.Infrastructure.Repositories.Aluno;

public class AlunoRepository : IAlunoReadOnlyRepository, IAlunoWriteOnlyRepository
{
    private readonly PortalEscolarDbContext _context;
    public AlunoRepository(PortalEscolarDbContext context)
    {
        _context = context;
    }

    public async Task AdicionarAsync(Domain.Entities.SalaAula.Aluno aluno) =>
        await _context.AddAsync(aluno);
    //TODO - ERRO ESTA AQUI
    public async Task<bool> ExisteEmailAsync(string emailUsuario) =>
        await _context.Aluno.AsNoTracking().AnyAsync(c => c.Email.Equals(emailUsuario));
}
