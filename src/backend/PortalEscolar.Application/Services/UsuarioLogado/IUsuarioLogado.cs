using PortalEscolar.Domain.Entities.Diretoria;

namespace PortalEscolar.Application.Services.UsuarioLogado;
public interface IUsuarioLogado
{
    Task<Diretor> ObterDiretor();
}
