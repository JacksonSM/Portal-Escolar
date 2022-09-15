using PortalEscolar.Communication.Request;
using PortalEscolar.Communication.Response;

namespace PortalEscolar.Application.UseCases.Professora.Login;
public interface ILoginProfessoraUseCase
{
    Task<ResponseTokenJson> ExecuteAsync(RequestUsuarioLoginJson request);
}
