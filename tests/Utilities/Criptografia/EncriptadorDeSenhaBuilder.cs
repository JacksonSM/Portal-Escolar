using PortalEscolar.Application.Services.Criptografia;

namespace Utilities.Criptografia;
public class EncriptadorDeSenhaBuilder
{
    public static EncriptadorDeSenha Instancia()
    {
        return new EncriptadorDeSenha("321ATB");
    }
}
