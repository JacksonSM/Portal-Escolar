using PortalEscolar.Domain.Entities.Papel;

namespace PortalEscolar.Domain.Interfaces.Repositories.Papeis;
public interface IPapelReadOnlyRepository
{
    Task<Papel> ObterPapel(string emailUsuario);
}
