using PortalEscolar.Application.Services.Criptografia;

namespace Utilities.Services.Criptografia;
public class EncriptadorDeSenhaBuilder
{
    public static EncriptadorDeSenha Instance()
    {
        return new EncriptadorDeSenha("OFNAzM@qz23pP1hJ0U%M%V@mT");
    }
}
