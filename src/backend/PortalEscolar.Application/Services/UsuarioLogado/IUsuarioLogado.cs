using PortalEscolar.Domain.Entities.Diretoria;
using PortalEscolar.Domain.Entities.SalaAula.ProfessoraContext;

namespace PortalEscolar.Application.Services.UsuarioLogado;
public interface IUsuarioLogado
{
    Task<Diretor> ObterDiretor();
    Task<Professora> ObterProfessora();
    Task<Domain.Entities.SalaAula.AlunoContext.Aluno> ObterAluno();
}
