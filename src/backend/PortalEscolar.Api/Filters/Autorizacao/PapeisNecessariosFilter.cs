using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using PortalEscolar.Application.Services.Token;
using PortalEscolar.Communication.Response;
using PortalEscolar.Domain.Interfaces.Repositories.Papeis;
using PortalEscolar.Exceptions;
using PortalEscolar.Exceptions.ExceptionsBase;

namespace PortalEscolar.Api.Filters.Autorizacao;

public class PapeisNecessariosFilter : IAsyncAuthorizationFilter
{
    private readonly TokenController _tokenController;
    private readonly IPapelReadOnlyRepository _repoReadPapel;

    readonly List<string> _papeisAutorizados;

    public PapeisNecessariosFilter(
        List<string> papeis, 
        TokenController tokenController, 
        IPapelReadOnlyRepository repoReadPapel)
    {
        _papeisAutorizados = papeis;
        _tokenController = tokenController;
        _repoReadPapel = repoReadPapel;
    }
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        try
        {
            var papelUsuario = await ObterPapelUsuario(context);

            var usuarioEstaAutorizado = _papeisAutorizados
                .Exists(papelAutorizado => papelAutorizado.ToUpper().Equals(papelUsuario.NomeNormalizado));

            
        }
        catch (SecurityTokenExpiredException)
        {
            TokenExpirado(context);
        }
        catch 
        {
            UsuarioNaoAutorizado(context);
        }
    }

    private void TokenExpirado(AuthorizationFilterContext context)
    {
        context.Result = new UnauthorizedObjectResult(new ResponseErroJson(ResourceMensagensDeErro.TOKEN_EXPIRADO));
    }

    private async Task<Domain.Entities.Papel.Papel> ObterPapelUsuario(AuthorizationFilterContext context)
    {
        var token = ObterTokenNaRequisicao(context);

        var emailUsuario = _tokenController.RecuperarEmail(token);

        var papelUsuario = await _repoReadPapel.ObterPapel(emailUsuario);

        return papelUsuario;
    }

    private static string ObterTokenNaRequisicao(AuthorizationFilterContext context)
    {
        var authorization = context.HttpContext.Request.Headers["Authorization"].ToString();

        if (string.IsNullOrWhiteSpace(authorization))
        {
            throw new PortalEscolarException(string.Empty);
        }

        return authorization["Bearer".Length..].Trim();
    }
    private static void UsuarioNaoAutorizado(AuthorizationFilterContext context)
    {
        context.Result = new UnauthorizedObjectResult(new ResponseErroJson(ResourceMensagensDeErro.USUARIO_NAO_AUTORIZADO));
    }


}
