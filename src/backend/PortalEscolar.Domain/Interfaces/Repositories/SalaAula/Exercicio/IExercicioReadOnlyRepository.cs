using PortalEscolar.Domain.Entities.SalaAula.ProfessoraContext.AtividadesParaResolver.Execicio;

namespace PortalEscolar.Domain.Interfaces.Repositories.SalaAula.Exercicio;
public interface IExercicioReadOnlyRepository
{
    Task<ExercicioParaResolver> ObterPorId(string id, long turmaId = 0);
    Task<List<ExercicioParaResolver>> ObterListaExercicios(ObterListaExerciciosQuery query);
}
