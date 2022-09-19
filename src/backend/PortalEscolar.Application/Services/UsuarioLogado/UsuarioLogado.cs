using Microsoft.AspNetCore.Http;
using PortalEscolar.Application.Services.Token;
using PortalEscolar.Domain.Entities.Diretoria;
using PortalEscolar.Domain.Entities.SalaAula;
using PortalEscolar.Domain.Interfaces.Repositories.Diretoria.Diretor;
using PortalEscolar.Domain.Interfaces.Repositories.SalaAula.Aluno;

namespace PortalEscolar.Application.Services.UsuarioLogado;
public class UsuarioLogado : IUsuarioLogado
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly TokenController _tokenController;
    private readonly IDiretorReadOnlyRepository _repoReadDiretor;

    public UsuarioLogado(IHttpContextAccessor httpContextAccessor,
        TokenController tokenController,
        IDiretorReadOnlyRepository repoReadDiretor)
    {
        _httpContextAccessor = httpContextAccessor;
        _tokenController = tokenController;
        _repoReadDiretor = repoReadDiretor;
    }
    public async Task<Diretor> ObterDiretor()
    {

        var token = ObterTokenRequisicao();

        var emailDiretor = _tokenController.RecuperarEmail(token);

        var diretor = await _repoReadDiretor.ObterPorEmailAsync(emailDiretor);

        return diretor;
    }
    private string ObterTokenRequisicao()
    {
        var authorization = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

        var token = authorization["Bearer".Length..].Trim();
        return token;
    }
}