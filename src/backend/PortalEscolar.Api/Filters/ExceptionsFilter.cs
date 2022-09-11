using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PortalEscolar.Communication.Response;
using PortalEscolar.Exceptions;
using PortalEscolar.Exceptions.ExceptionsBase;
using System.Net;

namespace PortalEscolar.Api.Filters;

public class ExceptionsFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is PortalEscolarException)
        {
            TratarMeuLivroDeReceitasException(context);
        }
        else
        {
            LancarErroDesconhecido(context);
        }
    }

    private static void TratarMeuLivroDeReceitasException(ExceptionContext context)
    {
        if (context.Exception is ErrosDeValidacaoException)
        {
            TratarErrosDeValidacaoException(context);
        }     
    }

    private static void TratarErrosDeValidacaoException(ExceptionContext context)
    {
        var erroDeValidacaoException = context.Exception as ErrosDeValidacaoException;

        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Result = new ObjectResult(new ResponseErroJson(erroDeValidacaoException.MensagensDeErro));
    }


    private static void LancarErroDesconhecido(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult(new ResponseErroJson(ResourceMensagensDeErro.ERRO_DESCONHECIDO));
    }

}
