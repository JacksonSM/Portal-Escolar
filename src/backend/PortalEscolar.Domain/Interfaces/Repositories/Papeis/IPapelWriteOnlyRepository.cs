using PortalEscolar.Domain.Entities.Papel;

namespace PortalEscolar.Domain.Interfaces.Repositories.Papeis;
public interface IPapelWriteOnlyRepository
{
    Task CriarPapelAsync(Papel papel);
    Task AplicarPapelAsync(string nomePapel,string emailUsuario);
}
