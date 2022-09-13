using PortalEscolar.Communication.Request;
using PortalEscolar.Communication.Response;

namespace PortalEscolar.Application.UseCases.Diretora.FazerLogin;

public interface IFazerLoginDiretorUseCase
{
    Task<ResponseTokenJson> ExecuteAsync(RequestUsuarioLoginJson request);
}