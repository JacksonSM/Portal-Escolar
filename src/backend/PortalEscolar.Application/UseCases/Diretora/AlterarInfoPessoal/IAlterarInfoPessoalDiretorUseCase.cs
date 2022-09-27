using PortalEscolar.Communication.Request.Diretor;
using PortalEscolar.Communication.Response;

namespace PortalEscolar.Application.UseCases.Diretora.AlterarInfoPessoal;
public interface IAlterarInfoPessoalDiretorUseCase
{
    Task<ResponseInfoPessoalDiretorJson> ExecuteAsync(RequestAlterarInfoPessoalDiretorJson request);
}
