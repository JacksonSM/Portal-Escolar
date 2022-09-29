using PortalEscolar.Communication.Response.Aluno.Exercicio;

namespace PortalEscolar.Application.UseCases.Aluno.ObterExercicio;
public interface IObterExercicioUseCase
{
    Task<ResponseExercicioParaResolverJson> ExecuteAsync(string exercicioId);
}
