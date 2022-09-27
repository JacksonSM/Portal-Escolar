using PortalEscolar.Communication.Request.Aluno;
using PortalEscolar.Communication.Response;

namespace PortalEscolar.Application.UseCases.Aluno.Registrar;
public interface IRegistrarAlunoUseCase
{
    Task<ReponseRegistarAlunoJson> ExecuteAsync(RequestRegistrarAlunoJson request);
}
