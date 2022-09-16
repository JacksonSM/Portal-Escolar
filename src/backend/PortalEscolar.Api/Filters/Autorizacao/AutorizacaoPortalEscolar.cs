using Microsoft.AspNetCore.Mvc;

namespace PortalEscolar.Api.Filters.Autorizacao;

public class AutorizacaoPortalEscolarAttribute : TypeFilterAttribute
{
    public AutorizacaoPortalEscolarAttribute(string papeisAutorizado) : base(typeof(PapeisNecessariosFilter))
    {
        var listaPapeis = papeisAutorizado.Split(',').ToList();
        Arguments = new object[] { listaPapeis };
    }
}
