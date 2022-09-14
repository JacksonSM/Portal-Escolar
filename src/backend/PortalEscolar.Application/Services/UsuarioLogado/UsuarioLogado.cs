using Microsoft.AspNetCore.Http;
using PortalEscolar.Application.Services.Token;
using PortalEscolar.Domain.Entities.Diretoria;
using PortalEscolar.Domain.Interfaces.Repositories.Diretoria.Diretor;

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
        var authorization = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

        var token = authorization["Bearer".Length..].Trim();

        var emailDiretor = _tokenController.RecuperarEmail(token);

        var diretor = await _repoReadDiretor.ObterPorEmailAsync("jackson@gmail.com");

        return diretor;
    }
}