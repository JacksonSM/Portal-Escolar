using PortalEscolar.Communication.Request.Professora;
using PortalEscolar.Communication.Response;

namespace PortalEscolar.Application.UseCases.Professora.Registrar;
public interface IRegistrarProfessoraUseCase
{
    Task<GenericResponseJson> ExecuteAsync(RequestRegistrarProfessoraJson request);
}
