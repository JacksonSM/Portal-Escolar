using PortalEscolar.Communication.Request;
using PortalEscolar.Communication.Response;

namespace PortalEscolar.Application.UseCases.Diretora.Login;

public interface ILoginDiretorUseCase
{
    Task<ResponseTokenJson> ExecuteAsync(RequestUsuarioLoginJson request);
}