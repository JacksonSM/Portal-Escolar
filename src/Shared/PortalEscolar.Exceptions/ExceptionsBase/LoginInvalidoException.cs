using System.Runtime.Serialization;

namespace PortalEscolar.Exceptions.ExceptionsBase;

[Serializable]
public class LoginInvalidoException : PortalEscolarException
{
    public LoginInvalidoException() : base(ResourceMensagensDeErro.LOGIN_INVALIDO)
    {

    }
    protected LoginInvalidoException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
