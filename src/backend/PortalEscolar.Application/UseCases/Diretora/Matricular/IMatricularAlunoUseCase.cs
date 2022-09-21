using PortalEscolar.Communication.Request.Matricula;
using PortalEscolar.Communication.Response;

namespace PortalEscolar.Application.UseCases.Diretora.Matricular;
public interface IMatricularAlunoUseCase
{
    Task<GenericResponseJson> ExecuteAsync(RequestMatricularAlunoJson request);
}
