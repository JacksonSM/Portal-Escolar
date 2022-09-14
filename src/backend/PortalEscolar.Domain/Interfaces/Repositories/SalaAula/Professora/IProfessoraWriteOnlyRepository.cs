namespace PortalEscolar.Domain.Interfaces.Repositories.SalaAula.Professora;
public interface IProfessoraWriteOnlyRepository
{
    Task AddAsync(Domain.Entities.SalaAula.Professora professora);
}
