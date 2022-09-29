using PortalEscolar.Communication.Request.Aluno;
using PortalEscolar.Communication.Response.Aluno.Exercicio;

namespace PortalEscolar.Application.UseCases.Aluno.ObterListaExercicios;
public interface IObterListaExercicios
{
    Task<List<ResponseExercicioParaResolverJson>> ExecuteAsync(RequestObterListaExerciciosQuery query);
}
