using PortalEscolar.Communication.Request.Aluno;
using PortalEscolar.Communication.Response.Aluno.ObterListaExercicios;

namespace PortalEscolar.Application.UseCases.Aluno.ObterListaExercicios;
public interface IObterListaExercicios
{
    Task<List<ResponseExercicioParaResolverJson>> ExecuteAsync(RequestObterListaExerciciosQuery query);
}
