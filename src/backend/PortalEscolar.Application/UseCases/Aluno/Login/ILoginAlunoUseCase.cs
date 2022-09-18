using PortalEscolar.Communication.Request;
using PortalEscolar.Communication.Response;

namespace PortalEscolar.Application.UseCases.Aluno.Login;

public interface ILoginAlunoUseCase
{
    Task<ResponseTokenJson> ExecuteAsync(RequestUsuarioLoginJson request);
}
