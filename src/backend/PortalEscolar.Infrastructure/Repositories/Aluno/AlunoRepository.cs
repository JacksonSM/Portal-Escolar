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

    public async Task AdicionarAsync(Domain.Entities.SalaAula.AlunoContext.Aluno aluno) =>
        await _context.AddAsync(aluno);
    public async Task<bool> ExisteEmailAsync(string emailUsuario) =>
        await _context.Aluno.AsNoTracking().AnyAsync(c => c.Email.Equals(emailUsuario));

    public async Task<Domain.Entities.SalaAula.AlunoContext.Aluno> ObterPorEmailAsync(string email) =>
        await _context.Aluno.AsNoTracking().FirstOrDefaultAsync(c => c.Email.ToUpper().Equals(email.ToUpper()));

    public async Task<Domain.Entities.SalaAula.AlunoContext.Aluno> ObterPorEmailSenhaAsync(string email, string senha) =>
        await _context.Aluno
        .AsNoTracking()
        .FirstOrDefaultAsync(c => c.Email.ToUpper().Equals(email.ToUpper()) && c.Senha.Equals(senha));
}
