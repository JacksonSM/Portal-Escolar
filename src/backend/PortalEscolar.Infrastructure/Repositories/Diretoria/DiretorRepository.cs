using Microsoft.EntityFrameworkCore;
using PortalEscolar.Domain.Entities.Diretoria;
using PortalEscolar.Domain.Interfaces.Repositories.Diretoria.Diretor;
using PortalEscolar.Infrastructure.Context;

namespace PortalEscolar.Infrastructure.Repositories.Diretoria;
public class DiretorRepository : IDiretorWriteOnlyRepository, IDiretorReadOnlyRepository
{
    private readonly PortalEscolarDbContext _context;

    public DiretorRepository(PortalEscolarDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Diretor diretor) =>
        await _context.AddAsync(diretor);

    public void Atualizar(Diretor diretor) =>
        _context.Diretor.Update(diretor);

    public async Task<bool> ExisteEmailAsync(string emailUsuario) =>
        await _context.Diretor.AsNoTracking().AnyAsync(c => c.Email.Equals(emailUsuario));

    public async Task<Diretor> ObterPorEmailAsync(string email) =>
        await _context.Diretor.AsNoTracking().FirstOrDefaultAsync(c => c.Email.Equals(email));

    public async Task<Diretor> ObterPorEmailSenhaAsync(string email, string senha) =>
        await _context.Diretor
        .AsNoTracking()
        .FirstOrDefaultAsync(c => c.Email.ToUpper().Equals(email.ToUpper()) && c.Senha.Equals(senha));
}
