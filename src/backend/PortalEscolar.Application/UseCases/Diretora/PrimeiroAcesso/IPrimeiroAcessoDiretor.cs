using PortalEscolar.Communication.Request;
using PortalEscolar.Communication.Response;

namespace PortalEscolar.Application.UseCases.Diretora.PrimeiroAcesso;
public interface IPrimeiroAcessoDiretor
{
    Task<ResponseTokenJson> ExecuteAsync(RequestUsuarioLoginJson request);
}
