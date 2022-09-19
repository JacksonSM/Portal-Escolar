using PortalEscolar.Communication.Request;
using PortalEscolar.Communication.Response;

namespace PortalEscolar.Application.UseCases.Diretora.AlterarInfoPessoal;
public interface IAlterarInfoPessoalDiretor
{
    Task<ResponseInfoPessoalDiretorJson> ExecuteAsync(RequestAlterarInfoPessoalDiretorJson request);
}
