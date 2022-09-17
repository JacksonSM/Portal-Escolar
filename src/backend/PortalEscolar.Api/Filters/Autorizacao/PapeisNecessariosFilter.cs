using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using PortalEscolar.Application.Services.Token;
using PortalEscolar.Communication.Response;
using PortalEscolar.Domain.Enum;
using PortalEscolar.Exceptions;
using PortalEscolar.Exceptions.ExceptionsBase;

namespace PortalEscolar.Api.Filters.Autorizacao;

public class PapeisNecessariosFilter : IAuthorizationFilter
{
    private readonly TokenController _tokenController;

    readonly Papel[] _papeisAutorizados;

    public PapeisNecessariosFilter(
        Papel[] papeis, 
        TokenController tokenController)
    {
        _papeisAutorizados = papeis;
        _tokenController = tokenController;
    }
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        try
        {
            var tokenRequest = ObterTokenNaRequisicao(context);

            var papelUsuario = _tokenController.RecuperarPapel(tokenRequest);

            bool papelEstaAutorizado = _papeisAutorizados
                .Any(papelAutorizado => papelAutorizado.Equals(papelUsuario));

            if (!papelEstaAutorizado) UsuarioNaoAutorizado(context); 
                     
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
