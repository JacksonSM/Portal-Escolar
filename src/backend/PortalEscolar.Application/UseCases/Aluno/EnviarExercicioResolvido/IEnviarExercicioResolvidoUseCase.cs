using PortalEscolar.Communication.Request.Aluno.EnviarExercicioResolvido;
using PortalEscolar.Communication.Response.Aluno.ExercicioResolvido;

namespace PortalEscolar.Application.UseCases.Aluno.EnviarExercicioResolvido;
public interface IEnviarExercicioResolvidoUseCase
{
    Task<ResponseExercicioResolvidoJson> ExecuteAsync(RequestEnviarExercicioResolvidoJson request);
}
