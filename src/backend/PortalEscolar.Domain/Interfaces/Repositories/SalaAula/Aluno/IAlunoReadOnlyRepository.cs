namespace PortalEscolar.Domain.Interfaces.Repositories.SalaAula.Aluno;
public interface IAlunoReadOnlyRepository
{
    Task<bool> ExisteEmailAsync(string email);
    Task<Entities.SalaAula.AlunoContext.Aluno> ObterPorEmailAsync(string email);
    Task<Entities.SalaAula.AlunoContext.Aluno> ObterPorEmailSenhaAsync(string email, string senha);

}
