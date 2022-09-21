namespace PortalEscolar.Domain.Interfaces.Repositories.Diretoria.Matricula;
public interface IMatriculaWriteOnlyRepository
{
    Task AdicionarAsync(Entities.Diretoria.Matricula.Matricula matricula);
}
