using PortalEscolar.Communication.Request;
using PortalEscolar.Communication.Response;

namespace PortalEscolar.Application.UseCases.Aluno.Registrar;
public interface IRegistrarAlunoUseCase
{
    Task<GenericResponseJson> ExecuteAsync(RequestRegistrarAlunoJson request);
}
