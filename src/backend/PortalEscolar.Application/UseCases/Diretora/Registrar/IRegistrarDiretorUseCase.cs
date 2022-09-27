using PortalEscolar.Communication.Request.Diretor;
using PortalEscolar.Communication.Response;

namespace PortalEscolar.Application.UseCases.Diretora.Registrar;
public interface IRegistrarDiretorUseCase 
{
    Task<ResponseTokenJson> ExecuteAsync(RequestRegistrarDiretorJson request);
}
