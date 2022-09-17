using Microsoft.AspNetCore.Mvc;
using PortalEscolar.Domain.Enum;

namespace PortalEscolar.Api.Filters.Autorizacao;

public class AutorizacaoPortalEscolarAttribute : TypeFilterAttribute
{
    public AutorizacaoPortalEscolarAttribute(Papel[] papeis) : base(typeof(PapeisNecessariosFilter))
    {
        Arguments = new object[] { papeis };
    }
}
