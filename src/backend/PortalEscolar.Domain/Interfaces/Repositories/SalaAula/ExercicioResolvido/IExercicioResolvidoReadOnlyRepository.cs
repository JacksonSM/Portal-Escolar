namespace PortalEscolar.Domain.Interfaces.Repositories.SalaAula.ExercicioResolvido;
public interface IExercicioResolvidoReadOnlyRepository
{
    Task<bool> ExercicioEstaResolvido(string exercicioResolvidoId, long alunoId);
}
