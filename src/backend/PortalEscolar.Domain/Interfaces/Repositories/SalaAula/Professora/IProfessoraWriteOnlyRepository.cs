namespace PortalEscolar.Domain.Interfaces.Repositories.SalaAula.Professora;
public interface IProfessoraWriteOnlyRepository
{
    Task AddAsync(Entities.SalaAula.ProfessoraContext.Professora professora);
}
