using PortalEscolar.Communication.Request;
using PortalEscolar.Communication.Response;

namespace PortalEscolar.Application.UseCases.Diretora.Registrar;
public interface IRegistrarDiretorUseCase 
{
    Task<ResponseTokenJson> ExecutarAsync(RequestRegistrarDiretorJson request);
}
