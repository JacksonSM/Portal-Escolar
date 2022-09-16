using PortalEscolar.Domain.Entities.Papel;
using PortalEscolar.Domain.Interfaces.Repositories.Papeis;
using PortalEscolar.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace PortalEscolar.Infrastructure.Repositories.Papel;
public class PapelRepository : IPapelWriteOnlyRepository, IPapelReadOnlyRepository
{
    private readonly PortalEscolarDbContext _context;

    public PapelRepository(PortalEscolarDbContext context)
    {
        _context = context;
    }
    public async Task AplicarPapelAsync(string nomePapel, string emailUsuario)
    {
        var papelSelecionado = await _context.Papel
            .FirstOrDefaultAsync(c => c.Nome.ToUpper().Equals(nomePapel.ToUpper()));

        var papelUsuario = new PapelUsuario { EmailUsuario = emailUsuario, Papel = papelSelecionado};

        await _context.PapelUsuario.AddAsync(papelUsuario);
    }

    public async Task CriarPapelAsync(Domain.Entities.Papel.Papel papel) =>
        await _context.Papel.AddAsync(papel);

    public async Task<Domain.Entities.Papel.Papel> ObterPapel(string emailUsuario)
    {
        var papel = await _context.PapelUsuario
            .Include(c => c.Papel)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.EmailUsuario.Equals(emailUsuario));

        return papel.Papel;
    }
}
