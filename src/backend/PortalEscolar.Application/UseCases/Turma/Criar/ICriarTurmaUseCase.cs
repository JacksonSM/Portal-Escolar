using PortalEscolar.Communication.Request.Diretor;
using PortalEscolar.Communication.Response;

namespace PortalEscolar.Application.UseCases.Turma.Criar;
public interface ICriarTurmaUseCase
{
    Task<GenericResponseJson> ExecuteAsync(RequestCriarTurmaJson request);
}
