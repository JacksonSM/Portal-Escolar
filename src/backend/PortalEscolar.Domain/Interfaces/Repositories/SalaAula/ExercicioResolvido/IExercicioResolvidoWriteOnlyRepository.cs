namespace PortalEscolar.Domain.Interfaces.Repositories.SalaAula.ExercicioResolvido;
public interface IExercicioResolvidoWriteOnlyRepository
{
    Task AdicionarAsync(Entities.SalaAula.AlunoContext.AtividadesResolvido.Exercicio.ExercicioResolvido exercicioResolvido);
}
