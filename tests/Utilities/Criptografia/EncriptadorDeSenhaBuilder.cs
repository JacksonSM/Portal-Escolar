using PortalEscolar.Application.Services.Criptografia;

namespace Utilities.Criptografia;
public class EncriptadorDeSenhaBuilder
{
    public static EncriptadorDeSenha Instance()
    {
        return new EncriptadorDeSenha("321ATB");
    }
}
