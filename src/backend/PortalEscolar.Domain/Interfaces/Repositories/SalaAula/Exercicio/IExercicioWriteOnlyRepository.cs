using PortalEscolar.Domain.Entities.SalaAula.ProfessoraContext.AtividadesParaResolver.Execicio;

namespace PortalEscolar.Domain.Interfaces.Repositories.SalaAula.Exercicio;
public interface IExercicioWriteOnlyRepository
{
    Task AdicionarAsync(ExercicioParaResolver exercicio);
}
