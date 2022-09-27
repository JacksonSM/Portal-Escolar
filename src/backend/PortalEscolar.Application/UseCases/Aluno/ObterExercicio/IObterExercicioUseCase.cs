using PortalEscolar.Communication.Response.Aluno.Exercicio;

namespace PortalEscolar.Application.UseCases.Aluno.ObterExercicio;
public interface IObterExercicioUseCase
{
    Task<ExercicioParaResolverJson> ExecuteAsync(string exercicioId);
}
