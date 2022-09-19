using PortalEscolar.Communication.Request;
using PortalEscolar.Communication.Response;

namespace PortalEscolar.Application.UseCases.Diretora.AlterarSenha;
public interface IAlterarSenhaDiretorUseCase 
{
    Task<GenericResponseJson> ExecuteAsync(RequestAlterarSenhaUsuarioJson request);
}
