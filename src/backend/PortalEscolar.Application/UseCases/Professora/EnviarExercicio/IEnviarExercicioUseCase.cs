using PortalEscolar.Communication.Request.Exercicio;
using PortalEscolar.Communication.Response;

namespace PortalEscolar.Application.UseCases.Professora.EnviarExercicio;
public interface IEnviarExercicioUseCase
{
    Task<GenericResponseJson> ExecuteAsync(RequestEnviarExercicioJson request);
}
