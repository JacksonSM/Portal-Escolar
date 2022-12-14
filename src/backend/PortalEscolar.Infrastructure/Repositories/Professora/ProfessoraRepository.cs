using Microsoft.EntityFrameworkCore;
using PortalEscolar.Domain.Interfaces.Repositories.SalaAula.Professora;
using PortalEscolar.Infrastructure.Context;

namespace PortalEscolar.Infrastructure.Repositories.Professora;
public class ProfessoraRepository : IProfessoraWriteOnlyRepository, IProfessoraReadOnlyRepository
{
    private readonly PortalEscolarDbContext _context;

    public ProfessoraRepository(PortalEscolarDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Domain.Entities.SalaAula.ProfessoraContext.Professora professora) =>
        await _context.Professora.AddAsync(professora);

    public async Task<bool> ExisteEmailAsync(string email) =>
        await _context.Professora.AnyAsync(c => c.Email.Equals(email));

    public async Task<bool> ExistePorIdAsync(long id) =>
        await _context.Professora.AnyAsync(c => c.Id == id);

    public async Task<Domain.Entities.SalaAula.ProfessoraContext.Professora> ObterPorEmailAsync(string email) =>
        await _context.Professora.AsNoTracking().FirstOrDefaultAsync(c => string.Equals(c.Email.ToUpper(),email.ToUpper()));

    public async Task<Domain.Entities.SalaAula.ProfessoraContext.Professora> ObterPorEmailESenhaAsync (string email, string senha) =>
        await _context.Professora
        .AsNoTracking()
        .FirstOrDefaultAsync(c => string.Equals(c.Email.ToUpper() , email.ToUpper()) && c.Senha.Equals(senha));
}
